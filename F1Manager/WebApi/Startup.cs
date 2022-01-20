using AutoMapper;
using Infrastructure.DataAccess;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.Text;
using WebApi.Extensions;
using WebApi.Utilities;

namespace WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            services.AddControllers().AddNewtonsoftJson(s =>
            {
                s.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            });
            services.AddControllers();
            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=f1m;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"),
            ServiceLifetime.Transient);

            services.Configure<Domain.Configurations.JWT.JwtSettings>(Configuration.GetSection("JwtSettings"));

            services.AddMapperConfiguration();

            services.AddScoped<Infrastructure.UnitOfWorks.Users.IUsersUoW, Infrastructure.UnitOfWorks.Users.UsersUoW>();
            services.AddScoped<Infrastructure.UnitOfWorks.Drivers.IDriversUnitOfWork, Infrastructure.UnitOfWorks.Drivers.DriversUoW>();
            services.AddScoped<Infrastructure.UnitOfWorks.ConstructorRacingDetails.IConstructorRacingDetailsUnitOfWork, Infrastructure.UnitOfWorks.ConstructorRacingDetails.ConstructorRacingDetailsUoW>();
            services.AddScoped<Infrastructure.UnitOfWorks.Countries.ICountriesUnitOfWork, Infrastructure.UnitOfWorks.Countries.CountriesUoW>();
            services.AddScoped<Infrastructure.UnitOfWorks.Constructors.ICounstructorUnitOfWork, Infrastructure.UnitOfWorks.Constructors.ConstructorUoW>();
            services.AddScoped<Infrastructure.UnitOfWorks.TechnicalStuffRole.ITechnicalStaffRoleUnitOfWork, Infrastructure.UnitOfWorks.TechnicalStuffRole.TechnicalStaffRoleUoW>();
            services.AddScoped<Infrastructure.UnitOfWorks.TechnicalStuff.ITechnicalStaffUnitOfWork, Infrastructure.UnitOfWorks.TechnicalStuff.TechnicalStaffUoW>();
            services.AddScoped<Infrastructure.UnitOfWorks.ConstructorsStaffContracts.IConstructorsStaffContractsUnitOfWork, Infrastructure.UnitOfWorks.ConstructorsStaffContracts.ConstructorsStaffContractsUoW>();

            services.AddScoped<Domain.Users.IUserRepository, Infrastructure.DataAccess.Repositores.UserRepository>();
            services.AddScoped<Domain.Constructors.IConstructorRepository, Infrastructure.DataAccess.Repositores.ConstructorRepository>();
            services.AddScoped<Domain.ConstructorRacingDetails.IConstructorRacingDetail, Infrastructure.DataAccess.Repositores.ConstructorRacingDetailRepository>();
            services.AddScoped<Domain.Countries.ICountryRepository, Infrastructure.DataAccess.Repositores.CountryRepository>();
            services.AddScoped<Domain.Drivers.IDriverRepository, Infrastructure.DataAccess.Repositores.DriverRepository>();
            services.AddScoped<Domain.TechnicalStaffRole.ITechnicalStaffRoleRepository, Infrastructure.DataAccess.Repositores.TechnicalStaffRoleRepository>();
            services.AddScoped<Domain.TechnicalStaff.ITechnicalStaffRepository, Infrastructure.DataAccess.Repositores.TechnicalStaffRepository>();
            services.AddScoped<Domain.ConstructorsStaffContracts.IConstructorsStaffContractsRepository, Infrastructure.DataAccess.Repositores.ConstructorStaffContractRepository>();


            services.TryAddTransient<IHttpContextAccessor, HttpContextAccessor>();


            // Auto Mapper Configurations
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new Mappers.ConstructorProfile());
                mc.AddProfile(new Mappers.DriverProfile());
                mc.AddProfile(new Mappers.UserProfile());
                mc.AddProfile(new Mappers.ConstructorResultProfile());
            });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);


            services.AddScoped<Application.Services.IUserService, Infrastructure.Authentication.UserService>();
            services.AddScoped<Application.Services.IJWTService, Infrastructure.Authentication.JWTService>();

            // configure jwt authentication
            var appSettingsSection = Configuration.GetSection("JwtSettings");
            var appSettings = appSettingsSection.Get<Domain.Configurations.JWT.JwtSettings>();

            services.AddAuthentication(o =>
            {
                o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = true,
                    ValidateIssuer = true,
                    ValidateLifetime = true,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(appSettings.Secret)),
                    ValidIssuer = appSettings.Issuer,
                    ValidAudience = appSettings.Audience
                };
            });

            services.AddAuthorization(config =>
            {
                config.AddPolicy("ShouldBeAAdmin", options =>
                {
                    options.RequireAuthenticatedUser();
                    options.AuthenticationSchemes.Add(JwtBearerDefaults.AuthenticationScheme);
                    options.Requirements.Add(new Infrastructure.Authentication.Requirements.ShouldBeAAdminRequirement());
                });
               /* options.AddPolicy("CanViewHome", policy => policy.RequireClaim(Constants.Strings.JwtClaimIdentifiers.Role, Constants.Strings.JwtClaims.Admin, Constants.Strings.JwtClaims.User, Constants.Strings.JwtClaims.ContentWriter, Constants.Strings.JwtClaims.PremiumUser));
                options.AddPolicy("ContentChanges", policy => policy.RequireClaim(Constants.Strings.JwtClaimIdentifiers.Role, Constants.Strings.JwtClaims.Admin, Constants.Strings.JwtClaims.ContentWriter));
                //options.AddPolicy("CanViewUsers", policy => policy.Requirements.Add(new RoleRequirement("Admin")));*/
            });

            services.AddSingleton<IAuthorizationHandler, Infrastructure.Authentication.Handlers.ShouldBeAAdminAuthorizationHandler> ();

            services.AddApiVersioning(setup =>
            {
                setup.DefaultApiVersion = new ApiVersion(1, 0);
                setup.AssumeDefaultVersionWhenUnspecified = true;
                setup.ReportApiVersions = true;
            });

            services.AddVersionedApiExplorer(setup =>
            {
                setup.GroupNameFormat = "'v'VVV";
                setup.SubstituteApiVersionInUrl = true;
            });

            services.AddSwaggerGen(c =>
            {
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, AppDbContext dbContext)
        {
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "SuperfundInvest v1"));


                app.UseDeveloperExceptionPage();
                app.UseSerilogRequestLogging();
            }

            app.UseCors(x => x
               .AllowAnyMethod()
               .AllowAnyHeader()
               .SetIsOriginAllowed(origin => true) // allow any origin
               );

            app.UseAuthentication();
            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Hello World!");
                });
            });

            dbContext.Database.EnsureCreated();
        }
    }
}
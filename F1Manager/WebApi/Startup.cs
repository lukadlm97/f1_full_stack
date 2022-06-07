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
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAllHeaders",
                    builder =>
                    {
                        builder.AllowAnyOrigin()
                            .AllowAnyHeader()
                            .AllowAnyMethod();
                    });
            });

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
            services.AddScoped<Infrastructure.UnitOfWorks.Competition.ICompetitionUnitOfWork, Infrastructure.UnitOfWorks.Competition.CompetitionUoW>();
            services.AddScoped<Infrastructure.UnitOfWorks.DriversContract.IDriversContractUnitOfWork, Infrastructure.UnitOfWorks.DriversContract.DriversContractUoW>();
            services.AddScoped<Infrastructure.UnitOfWorks.PowerUnitSupplier.IPowerUnitSupplierUnitOfWork, Infrastructure.UnitOfWorks.PowerUnitSupplier.PowerUnitSupplierUoW>();
            services.AddScoped<Infrastructure.UnitOfWorks.ConstructorsPowerUnit.IConstructorsPowerUnit, Infrastructure.UnitOfWorks.ConstructorsPowerUnit.ConstructorsPowerUnitUoW>();
            services.AddScoped<Infrastructure.UnitOfWorks.DriverRole.IDriverRoleUnitOfWork, Infrastructure.UnitOfWorks.DriverRole.DriverRoleUoW>(); services.AddScoped<Infrastructure.UnitOfWorks.Season.ISeasonUnitOfWork, Infrastructure.UnitOfWorks.Season.SeasonUoW>();

            services.AddScoped<Domain.Users.IUserRepository, Infrastructure.DataAccess.Repositores.UserRepository>();
            services.AddScoped<Domain.Constructors.IConstructorRepository, Infrastructure.DataAccess.Repositores.ConstructorRepository>();
            services.AddScoped<Domain.ConstructorRacingDetails.IConstructorRacingDetail, Infrastructure.DataAccess.Repositores.ConstructorRacingDetailRepository>();
            services.AddScoped<Domain.Countries.ICountryRepository, Infrastructure.DataAccess.Repositores.CountryRepository>();
            services.AddScoped<Domain.Drivers.IDriverRepository, Infrastructure.DataAccess.Repositores.DriverRepository>();
            services.AddScoped<Domain.TechnicalStaffRole.ITechnicalStaffRoleRepository, Infrastructure.DataAccess.Repositores.TechnicalStaffRoleRepository>();
            services.AddScoped<Domain.TechnicalStaff.ITechnicalStaffRepository, Infrastructure.DataAccess.Repositores.TechnicalStaffRepository>();
            services.AddScoped<Domain.ConstructorsStaffContracts.IConstructorsStaffContractsRepository, Infrastructure.DataAccess.Repositores.ConstructorStaffContractRepository>();
            services.AddScoped<Domain.RacingChampionship.IRacingChampionshipRepository, Infrastructure.DataAccess.Repositores.RacingChampionshipRepository>();
            services.AddScoped<Domain.Contracts.IContractRepository, Infrastructure.DataAccess.Repositores.ContractRepository>();
            services.AddScoped<Domain.PoweUnitSupplier.IPowerUnitSupplier, Infrastructure.DataAccess.Repositores.PowerUnitSupplierRepository>();
            services.AddScoped<Domain.ConstructorsPowerUnits.IConstructorsPowerUnit, Infrastructure.DataAccess.Repositores.ConstructorsPowerUnitRepository>();
            services.AddScoped<Domain.DriverRoles.IDriverRolesRepository, Infrastructure.DataAccess.Repositores.DriverRolesRepository>();
            services.AddScoped<Domain.Season.ISeasonRepository, Infrastructure.DataAccess.Repositores.SeasonRepository>();


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

            services.AddSingleton<IAuthorizationHandler, Infrastructure.Authentication.Handlers.ShouldBeAAdminAuthorizationHandler>();

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
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "F1 Manager v1");
                    c.SwaggerEndpoint("/swagger/v2/swagger.json", "F1 Manager v2");
                });

                app.UseReDoc(c =>
                {
                    c.DocumentTitle = "F1 Manager API Documentation";
                    c.SpecUrl = "/swagger/v1/swagger.json";
                });

                app.UseDeveloperExceptionPage();
                app.UseSerilogRequestLogging();

            }

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseRouting(); 
            app.UseCors("AllowAllHeaders");
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
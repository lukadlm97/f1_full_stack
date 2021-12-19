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
using System;
using System.Text;
using WebApi.AuthorizationAssets;
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

            services.AddMapperConfiguration();

            services.AddScoped<Infrastructure.UnitOfWorks.Users.IUsersUoW, Infrastructure.UnitOfWorks.Users.UsersUoW>();
            services.AddScoped<Infrastructure.UnitOfWorks.Drivers.IDriversUnitOfWork, Infrastructure.UnitOfWorks.Drivers.DriversUoW>();
            services.AddScoped<Infrastructure.UnitOfWorks.ConstructorRacingDetails.IConstructorRacingDetailsUnitOfWork, Infrastructure.UnitOfWorks.ConstructorRacingDetails.ConstructorRacingDetailsUoW>();
            services.AddScoped<Infrastructure.UnitOfWorks.Countries.ICountriesUnitOfWork, Infrastructure.UnitOfWorks.Countries.CountriesUoW>();
            services.AddScoped<Infrastructure.UnitOfWorks.Constructors.ICounstructorUnitOfWork, Infrastructure.UnitOfWorks.Constructors.ConstructorUoW>();

            services.AddScoped<Domain.Users.IUserRepository, Infrastructure.DataAccess.Repositores.UserRepository>();
            services.AddScoped<Domain.Constructors.IConstructorRepository, Infrastructure.DataAccess.Repositores.ConstructorRepository>();
            services.AddScoped<Domain.ConstructorRacingDetails.IConstructorRacingDetail, Infrastructure.DataAccess.Repositores.ConstructorRacingDetailRepository>();
            services.AddScoped<Domain.Countries.ICountryRepository, Infrastructure.DataAccess.Repositores.CountryRepository>();
            services.AddScoped<Domain.Drivers.IDriverRepository, Infrastructure.DataAccess.Repositores.DriverRepository>();

            services.AddSingleton<IJwtFactory, JwtFactory>();

            services.TryAddTransient<IHttpContextAccessor, HttpContextAccessor>();


            // Auto Mapper Configurations
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new Mappers.ConstructorProfile());
                mc.AddProfile(new Mappers.DriverProfile());
                mc.AddProfile(new Mappers.UserProfile());
            });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);


            SymmetricSecurityKey _signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration["AppSettings:Secret"]));
            var jwtAppSettingOptions = Configuration.GetSection(nameof(JwtIssuerOptions));

            // Configure JwtIssuerOptions
            services.Configure<JwtIssuerOptions>(options =>
            {
                options.Issuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)];
                options.Audience = jwtAppSettingOptions[nameof(JwtIssuerOptions.Audience)];
                options.SigningCredentials = new SigningCredentials(_signingKey, SecurityAlgorithms.HmacSha256);
            });

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)],

                ValidateAudience = true,
                ValidAudience = jwtAppSettingOptions[nameof(JwtIssuerOptions.Audience)],

                ValidateIssuerSigningKey = true,
                IssuerSigningKey = _signingKey,

                RequireExpirationTime = false,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(configureOptions =>
            {
                configureOptions.ClaimsIssuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)];
                configureOptions.TokenValidationParameters = tokenValidationParameters;
                configureOptions.SaveToken = true;
            });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("CanViewUsers", policy => policy.RequireClaim(Constants.Strings.JwtClaimIdentifiers.Role, Constants.Strings.JwtClaims.Admin));
                options.AddPolicy("CanViewHome", policy => policy.RequireClaim(Constants.Strings.JwtClaimIdentifiers.Role, Constants.Strings.JwtClaims.Admin, Constants.Strings.JwtClaims.User, Constants.Strings.JwtClaims.ContentWriter, Constants.Strings.JwtClaims.PremiumUser));
                options.AddPolicy("ContentChanges", policy => policy.RequireClaim(Constants.Strings.JwtClaimIdentifiers.Role, Constants.Strings.JwtClaims.Admin, Constants.Strings.JwtClaims.ContentWriter));
                //options.AddPolicy("CanViewUsers", policy => policy.Requirements.Add(new RoleRequirement("Admin")));
            });

            services.AddSingleton<IAuthorizationHandler, RoleHandler>();

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
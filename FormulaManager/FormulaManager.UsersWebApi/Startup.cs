using FormulaManager.BusinessLogic.Repository.Repositories;
using FormulaManager.BusinessLogic.Services.Services;
using FormulaManager.DAL.Entities.Configurations;
using FormulaManager.DAL.Entities.Repositories;
using FormulaManager.DAL.Entities.Services;
using FormulaManager.DAL.Entities.Users;
using FormulaManager.DAL.Persistance;
using FormulaManager.UsersWebApi.Automappers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Diagnostics;
using System.Reflection;

namespace FormulaManager.UsersWebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        private readonly string Cors = "Policy";

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(Cors,
                builder =>
                {
                    builder.AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod();
                });
            });

            services.AddControllers();

            services.AddScoped<IUserService, UsersService>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IDataService<User>, GenericDataRepository<User>>();
            services.Configure<DbConfiguration>(Configuration.GetSection("DbConfiguration"));
            services.AddScoped<AppDbContextFactory>();
            services.AddAutoMapper(typeof(UserProfile));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(Cors);

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync($"FormulaManager version: {FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).FileVersion}");
                });
                endpoints.MapControllers();
            });
        }
    }
}
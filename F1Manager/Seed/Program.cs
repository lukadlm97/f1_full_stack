using Infrastructure.DataAccess;
using Infrastructure.UnitOfWorks.Countries;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Seed.CountrySeed;
using System;
using System.Threading.Tasks;

namespace Seed
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            var builder = new HostBuilder()
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddLogging(configure => configure.AddConsole())
                   .AddScoped<CountriesUoW, CountriesUoW>()
                   .AddDbContext<AppDbContext>(options =>
                     {
                         options.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=f1m;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
                     });
                }).UseConsoleLifetime();

            var host = builder.Build();

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var context = services.GetRequiredService<AppDbContext>();
                    context.Database.Migrate();
                    SeedData.SetRetairDrivers(context);
                    //SeedData.SetActiveDrivers(context);
                    //SeedData.SeedCountriesData(context);
                    //await SeedData.SeedDriverssData(context);
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occured during migration");
                }
            }
            host.Run();
        }
    }
}
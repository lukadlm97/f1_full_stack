using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Serilog;
using System.Threading.Tasks;

namespace WebApi
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            await CreateHostBuilder(args).Build().RunAsync();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host
            .CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                    webBuilder.UseUrls("http://localhost:6000", "https://localhost:6001");
                    webBuilder.UseKestrel();
                    webBuilder.UseSerilog((ctx, lc) => lc
                                        .WriteTo.Console()
                                        .ReadFrom.Configuration(ctx.Configuration));
                });
    }
}
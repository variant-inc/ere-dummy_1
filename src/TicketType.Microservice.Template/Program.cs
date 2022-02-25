using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace TicketType.Microservice.Template
{
    [ExcludeFromCodeCoverage]
    public class Program
    {
        public static async Task Main(string[] args)
        {
            using var host = CreateHostBuilder(args)
                .UseConsoleLifetime()
                .Build();
            await host.RunAsync();
        }

        private static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSerilog((hostContext, logConfiguration) =>
                    logConfiguration.ReadFrom.Configuration(hostContext.Configuration)
                )
				.ConfigureServices(Startup.ConfigureServices);
    }
}
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace TicketType.Microservice.Template
{
    [ExcludeFromCodeCoverage]
    public static class Program
    {
        public static async Task Main(string[] args)
        {
            using var host = CreateHostBuilder(args).UseConsoleLifetime().Build();

            using var scope = host.Services.CreateScope();

            await host.RunAsync();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices(Startup.ConfigureServices)
                .UseSerilog((hostContext, logConfiguration) =>
                    logConfiguration.ReadFrom.Configuration(hostContext.Configuration)
                );
    }
}
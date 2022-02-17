using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace TicketType.Microservice.Template
{
    [ExcludeFromCodeCoverage]
    public static class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().RunAsync();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices(Startup.ConfigureServices)
                .UseSerilog((hostContext, logConfiguration) =>
                    logConfiguration.ReadFrom.Configuration(hostContext.Configuration)
                );
    }
}
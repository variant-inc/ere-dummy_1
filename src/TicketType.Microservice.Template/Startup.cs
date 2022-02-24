using System.Diagnostics.CodeAnalysis;
using Epsagon.Dotnet.Instrumentation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using TicketType.Microservice.Template.Handlers;
using Variant.TicketsShared.Messaging.DependencyInjection;

namespace TicketType.Microservice.Template
{
    [ExcludeFromCodeCoverage]
    public static class Startup
    {
        static Startup()
        {
            EpsagonBootstrap.Bootstrap();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public static void ConfigureServices(HostBuilderContext hostContext, IServiceCollection services)
        {
            var config = hostContext.Configuration;
            services.AddMessagingServices<EntitySqsQueueHandler>(config);
            // services.ConfigureLaunchDarkly(config);

			var provider = services.BuildServiceProvider();
            var logger = provider.GetRequiredService<ILogger>();
            logger.Information("Services Configured!");
        }
    }
}
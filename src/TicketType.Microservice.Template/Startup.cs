using System;
using System.Diagnostics.CodeAnalysis;
using Epsagon.Dotnet.Instrumentation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using TicketType.Microservice.Template.Handlers;
using TicketType.Microservice.Template.Infrastructure.FeatureFlags;
using Variant.TicketsShared.LaunchDarklyExtensions;
using TicketType.Microservice.Template.Infrastructure;
using Variant.TicketsShared.DataSource.Infrastructure;
using Variant.TicketsShared.Messaging.DependencyInjection;
using TicketType.Microservice.Template.Services;

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

            services.AddAutoMapper(cfg => cfg.AddMaps(AppDomain.CurrentDomain.GetAssemblies()));

            services.AddDataSources(config);

            services.AddMessagingServices<EntitySqsQueueHandler>(config);
            // services.ConfigureLaunchDarkly(config);

            services.AddScoped<GeneratorErrorHandler>();

            var provider = services.BuildServiceProvider();
            var logger = provider.GetRequiredService<ILogger>();
            logger.Information("Services Configured!");
        }
    }
}
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Diagnostics.CodeAnalysis;
using TicketType.Microservice.Template.Infrastructure.FeatureFlags.Abstract;
using TicketType.Microservice.Template.Infrastructure.FeatureFlags.Flags;
using TicketType.Microservice.Template.Infrastructure.FeatureFlags.Services;

namespace TicketType.Microservice.Template.Modules
{
    [ExcludeFromCodeCoverage]
    public static class FeatureFlagsModule
    {
        public static IServiceCollection AddFeatureFlags(this IServiceCollection services, IConfiguration configuration)
        {
            Configure(configuration,services);
            RegisterServices(services);
            RegisterFlags(services);

            return services;
        }

        private static void Configure(IConfiguration configuration, IServiceCollection services)
        {
            if (configuration.GetValue<string>("DOTNET_ENVIRONMENT") == null) return;

            var section = configuration.GetSection("LaunchDarkly");
            string key = section.GetValue<string>("Key");
            string userName = section.GetValue<string>("UserName");
            var loggerFactory = services.BuildServiceProvider().GetRequiredService<ILoggerFactory>();
            LdFeatureFlagsService.Initialize(key, userName,loggerFactory);
        }

        private static void RegisterServices(IServiceCollection services)
        {
            services.AddSingleton<LdFeatureFlagsService>();
            services.AddSingleton<IBooleanFeatureFlagValueProvider>(x => x.GetRequiredService<LdFeatureFlagsService>());
            services.AddSingleton<IStringFeatureFlagValueProvider>(x => x.GetRequiredService<LdFeatureFlagsService>());
        }

        private static void RegisterFlags(IServiceCollection services)
        {
            // Here register feature flags classes
            // services.AddSingleton<YourNewAwesomeFeatureFlag>();
        }
    }
}

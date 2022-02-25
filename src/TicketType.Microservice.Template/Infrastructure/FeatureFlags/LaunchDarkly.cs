using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;
using Variant.TicketsShared.LaunchDarklyExtensions;

namespace TicketType.Microservice.Template.Infrastructure.FeatureFlags
{
    [ExcludeFromCodeCoverage]
    internal static class LaunchDarkly
    {
        public static IServiceCollection ConfigureLaunchDarkly(this IServiceCollection services, IConfiguration config)
        {
            var ldSection = config.GetSection("LaunchDarkly");
            var ldConfig = new LaunchDarklyConfiguration
            {
                Key = ldSection.GetValue<string>("Key"),
                UserName = ldSection.GetValue<string>("UserName")
            };

            services.AddFeatureFlags(ldConfig);

            return services;
        }
    }
}

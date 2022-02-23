using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Variant.TicketsShared.DataSource.Infrastructure;

namespace TicketType.Microservice.Template.Infrastructure
{
    internal static class DataSourcesRegistration
    {

        public static void AddDataSources(this IServiceCollection services, IConfiguration configuration)
        {
            AddTractorSource(services, configuration);
        }

        private static (string baseAddress, string userAgent) GetEntityApiConfig(IConfiguration configuration)
        {
            var apiSection = configuration.GetSection("EntityApi");
            return (
                apiSection.GetValue<string>("BaseAddress"),
                apiSection.GetValue<string>("UserAgent")
                );
        }

        private static void AddTractorSource(IServiceCollection services, IConfiguration configuration)
        {
            var (baseAddress, userAgent) = GetEntityApiConfig(configuration);
            var resourcePath = configuration.GetSection("TractorApi").GetValue<string>("ResourcePath");

            var tractorDataConfig = new HttpEntityDataSourceConfiguration
            {
                Domain = baseAddress,
                ResourcePath = resourcePath,
                UserAgent = userAgent
            };

            services.AddTractorDataService(tractorDataConfig);
        }
    }
}

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Diagnostics.CodeAnalysis;
using Variant.TicketsShared.DataSource.Infrastructure;
using Variant.TicketsShared.DataSource.Simulation.Abstract;
using Variant.TicketsShared.DataSource.Simulation.RequestModels;

namespace TicketType.Microservice.Template.Infrastructure
{
    [ExcludeFromCodeCoverage]
    internal static class DataSourcesRegistration
    {
        public static IServiceCollection AddDataSources(this IServiceCollection services, IConfiguration configuration)
        {
            //AddTractorSource(services, configuration);
            //AddDriverSource(services, configuration);
            //AddHomeTimeSource(services, configuration);
            //AddOrderSource(services, configuration);
            AddSimulationsDataSource(services, configuration);
            return services;
        }

        private static void AddSimulationsDataSource(IServiceCollection services, IConfiguration configuration)
        {
            var baseAddress = configuration.GetSection("SimulationsApi").GetValue<string>("BaseAddress");
            services.AddSimulationAssignmentsDataService(new HttpDataSourceDomainConfiguration { Domain = baseAddress });

            var s = services.BuildServiceProvider().GetRequiredService<ISimulationData>();
            var searchParams = new SimulationTractorSearchParams() { CreationDatetimeGreaterThan = new DateTime(2021, 11, 24, 16, 12, 49) };
            var res = s.SearchTractorSimulationAssignments(searchParams).Result;
        }

        private static void AddTractorSource(IServiceCollection services, IConfiguration configuration) => services.AddEntityDataSource(configuration, "TractorApi", services.AddTractorDataService);

        private static void AddDriverSource(IServiceCollection services, IConfiguration configuration) => services.AddEntityDataSource(configuration, "DriverApi", services.AddDriversDataService);

        private static void AddHomeTimeSource(IServiceCollection services, IConfiguration configuration) => services.AddEntityDataSource(configuration, "HometimeApi", services.AddHomeTimeDataService);

        private static void AddOrderSource(IServiceCollection services, IConfiguration configuration) => services.AddEntityDataSource(configuration, "OrderApi", services.AddOrdersDataService);

        private static void AddEntityDataSource(this IServiceCollection services, IConfiguration configuration, string configSection, Func<HttpEntityDataSourceConfiguration, IServiceCollection> bootstrapingMethod)
        {
            var (baseAddress, userAgent) = GetEntityApiConfig(configuration);
            var resourcePath = configuration.GetSection(configSection).GetValue<string>("ResourcePath");

            var sourceConfig = new HttpEntityDataSourceConfiguration
            {
                Domain = baseAddress,
                ResourcePath = resourcePath,
                UserAgent = userAgent
            };

            bootstrapingMethod(sourceConfig);
        }

        private static (string baseAddress, string userAgent) GetEntityApiConfig(IConfiguration configuration)
        {
            var apiSection = configuration.GetSection("EntityApi");
            return (
                apiSection.GetValue<string>("BaseAddress"),
                apiSection.GetValue<string>("UserAgent")
                );
        }


    }
}

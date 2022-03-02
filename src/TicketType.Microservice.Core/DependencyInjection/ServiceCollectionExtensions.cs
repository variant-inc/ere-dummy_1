using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;
using Variant.TicketsShared.Messaging.Interfaces;

namespace TicketType.Microservice.Core.DependencyInjection
{
    [ExcludeFromCodeCoverage]
    /// <summary>Extensions for registering this project in a microservice.</summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Adds a message handler backed by SQS to the service collection and the check list
        /// singleton.
        /// </summary>
        /// <param name="services">Service container to register the message handler into</param>
        public static IServiceCollection AddCoreDependencies(
            this IServiceCollection services)
        {
            services.AddScoped<IBusinessLogic, BusinessLogic>();

            return services;
        }
    }
}
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TicketType.Microservice.Core.Exceptions;
using TicketType.Microservice.Core.Helpers;
using TicketType.Microservice.Core.Interfaces;
using TicketType.Microservice.Core.PublishMessage;

namespace TicketType.Microservice.Core.DependencyInjection
{
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
            services.AddScoped<IBusinessLogic, BusinessLogic>()
                .AddScoped<ICreateTickets, CreateTickets>()
                .AddScoped<IGetDataFromApi, GetDataFromApi>()
                .AddScoped<IPublishMessages, PublishMessages>();

            return services;
        }
    }
}
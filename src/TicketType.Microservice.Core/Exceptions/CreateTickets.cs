using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Logging;
using TicketType.Microservice.Core.Interfaces;
using TicketType.Microservice.Core.Models;
using Variant.TicketsShared.DataSource.Driver;
using Variant.TicketsShared.DataSource.Order.ResponseModels;
using Variant.TicketsShared.DataSource.Tractor.ResponseModels;

namespace TicketType.Microservice.Core.Exceptions
{
    [ExcludeFromCodeCoverage]
    public class CreateTickets : ICreateTickets
    {
        private readonly IBusinessLogic _businessLogic;
        private readonly ILogger _logger;
        
        public CreateTickets(
            IBusinessLogic businessLogic,
            ILogger<CreateTickets> logger)
        {
            _businessLogic = businessLogic;
            _logger = logger;
        }
        
        public List<TicketMessage> CreateTicketsFromTractorData(IEnumerable<TractorData> tractorData)
        {
            var ticketMessages = new List<TicketMessage>();
            // Write logic to check if each tractorData matches the requirements for ticketType
            foreach (TractorData data in tractorData)
            {
                if (!_businessLogic.IsTractorDataMatchesTheTicketRequirements(data))
                {
                    continue;
                }
                var newTicketMessage = new TicketMessage
                {
                    // Add the required attributes to form a ticket
                };
                ticketMessages.Add(newTicketMessage);
            }
            
            return ticketMessages;
        }
        
        public List<TicketMessage> CreateTicketsFromDriverData(IEnumerable<DriverData> driverData)
        {
            var ticketMessages = new List<TicketMessage>();
            // Write logic to check if each driverData matches the requirements for ticketType
            foreach (DriverData data in driverData)
            {
                if (!_businessLogic.IsDriverDataMatchesTheTicketRequirements(data)) {
                    continue;
                }
                var newTicketMessage = new TicketMessage
                {
                    // Add the required attributes to form a ticket
                };
                ticketMessages.Add(newTicketMessage);
            }
            
            return ticketMessages;
        }
        
        public List<TicketMessage> CreateTicketsFromOrderData(IEnumerable<OrderData> orderData)
        {
            var ticketMessages = new List<TicketMessage>();
            // Write logic to check if each orderData matches the requirements for ticketType
            foreach (OrderData data in orderData)
            {
                if (!_businessLogic.IsOrderDataMatchesTheTicketRequirements(data))
                {
                    continue;  
                }
                var newTicketMessage = new TicketMessage
                {
                    // Add the required attributes to form a ticket
                };
                ticketMessages.Add(newTicketMessage);
            }
            
            return ticketMessages;
        }
    }
}
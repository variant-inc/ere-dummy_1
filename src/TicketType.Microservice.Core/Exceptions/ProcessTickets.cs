using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using TicketType.Microservice.Core.Helpers;
using TicketType.Microservice.Core.Models;
using TicketType.Microservice.Core.Interfaces;
using Variant.TicketsShared.Messaging.Models;
using Variant.TicketsShared.Messaging.PublishMessage;

namespace TicketType.Microservice.Core.Exceptions
{
    public class ProcessTickets : IProcessTickets
    {
        private readonly ILogger _logger;
        private readonly IGetDataFromApi _getDataFromApi;
        private readonly ICreateTickets _createTickets;
        private readonly IPublishMessages _publishMessages;
        
        public ProcessTickets(ILogger<ProcessTickets> logger, ICreateTickets createTickets, 
            IGetDataFromApi getDataFromApi,IPublishMessages publishMessages)
        {
            _logger = logger;
            _createTickets = createTickets;
            _getDataFromApi = getDataFromApi;
            _publishMessages = publishMessages;
        }

        public async Task ProcessDriverTickets()
        {
            try
            {
                var driverData = await _getDataFromApi.GetDataFromDriverApi();
                var tickets = _createTickets.CreateTicketsFromDriverData(driverData);
                await _publishMessages.PublishMessageToSnsTopic(tickets);
            }
            catch(Exception ex)
            {
                _logger.LogError("Failed to process Drivers Data");
            }
        }
        
        public async Task ProcessTractorTickets()
        {
            try
            {
                var driverData = await _getDataFromApi.GetDataFromTractorApi();
                var tickets = _createTickets.CreateTicketsFromTractorData(driverData);
                await _publishMessages.PublishMessageToSnsTopic(tickets);
            }
            catch(Exception ex)
            {
                _logger.LogError("Failed to process Drivers Data");
            }
        }
        
        public async Task ProcessOrderTickets()
        {
            try
            {
                var driverData = await _getDataFromApi.GetDataFromOrderApi();
                var tickets = _createTickets.CreateTicketsFromOrderData(driverData);
                await _publishMessages.PublishMessageToSnsTopic(tickets);
            }
            catch(Exception ex)
            {
                _logger.LogError("Failed to process Drivers Data");
            }
        }
    }
}
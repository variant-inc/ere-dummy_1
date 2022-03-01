using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using TicketType.Microservice.Core.Interfaces;

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

        public async Task ProcessDriverTickets(string topicName)
        {
            try
            {
                var driverData = await _getDataFromApi.GetDataFromDriverApi();
                var tickets = _createTickets.CreateTicketsFromDriverData(driverData);
                await _publishMessages.PublishMessageToSnsTopic(topicName,tickets);
            }
            catch(Exception ex)
            {
                _logger.LogError("Failed to process Drivers Data");
            }
        }
        
        public async Task ProcessTractorTickets(string topicName)
        {
            try
            {
                var driverData = await _getDataFromApi.GetDataFromTractorApi();
                var tickets = _createTickets.CreateTicketsFromTractorData(driverData);
                await _publishMessages.PublishMessageToSnsTopic(topicName, tickets);
            }
            catch(Exception ex)
            {
                _logger.LogError("Failed to process Drivers Data");
            }
        }
        
        public async Task ProcessOrderTickets(string topicName)
        {
            try
            {
                var driverData = await _getDataFromApi.GetDataFromOrderApi();
                var tickets = _createTickets.CreateTicketsFromOrderData(driverData);
                await _publishMessages.PublishMessageToSnsTopic(topicName, tickets);
            }
            catch(Exception ex)
            {
                _logger.LogError("Failed to process Drivers Data");
            }
        }
    }
}
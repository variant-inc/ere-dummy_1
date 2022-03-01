using System.Data;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using TicketType.Microservice.Core.Helpers;
using TicketType.Microservice.Core.Interfaces;
using Variant.TicketsShared.Messaging.Interfaces;

namespace TicketType.Microservice.Core
{
    public class DataHandler : IDataHandler
    {
        private readonly ILogger _logger;
        private readonly IProcessTickets _processTickets;
        
        public DataHandler(ILogger<DataHandler> logger, IProcessTickets processTickets)
        {
            _logger = logger;
            _processTickets = processTickets;
        }
        
        public Task ManageChecklistAsync(string topicName, IEntityApiChecklist checklist)
        {
            // This value can't be modified after this
            GlobalVars.SnsTopicName = topicName;
            
            if (checklist.DriverApiReady)
            {
                _processTickets.ProcessDriverTickets();
            }else if (checklist.TractorApiReady)
            {
                _processTickets.ProcessTractorTickets();
            }else if (checklist.OrderApiReady)
            {
                _processTickets.ProcessOrderTickets();
            }
            else
            {
                throw new DataException("checklist must have either one of the event to be true");
            }

            return Task.CompletedTask;
        }
        
    }
}
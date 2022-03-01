using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using TicketType.Microservice.Core;
using TicketType.Microservice.Core.Interfaces;
using Variant.TicketsShared.Messaging.Abstracts;
using Variant.TicketsShared.Messaging.Interfaces;
using Variant.TicketsShared.Messaging.PublishMessage;

namespace TicketType.Microservice.Template.Handlers
{
    [ExcludeFromCodeCoverage]
    public class EntitySqsQueueHandler : AbstractSQSHandler
    {
        private readonly IProcessTickets _processTickets;
        
        public EntitySqsQueueHandler(
            ILogger<EntitySqsQueueHandler> logger,
            IEntityApiChecklist checklist,
            IPublishMessageToSNSTopic publisher,
            IProcessTickets processTickets
        ) : base(logger, checklist, publisher)
        {
            _processTickets = processTickets;
            logger.LogInformation("EntitySqsQueueHandler started");
        }

        // Required by IMessageHandler
        public override async Task Init()
        {
            _logger.LogInformation("Initializing template microservice");
            
            try
            {
                 await ManageChecklistAsync("Outgoing",_checklist);
            }
            catch
            {
                _logger.LogError("Error Handling Data fro the Queue");
            }
            
        }
        
        private async Task ManageChecklistAsync(string topicName, IEntityApiChecklist checklist)
        {
            if (checklist.DriverApiReady)
            {
                await _processTickets.ProcessDriverTickets(topicName);
            }else if (checklist.TractorApiReady)
            {
                await _processTickets.ProcessTractorTickets(topicName);
            }else if (checklist.OrderApiReady)
            {
                await _processTickets.ProcessOrderTickets(topicName);
            }
            else
            {
                throw new DataException("checklist must have either one of the event to be true");
            }
        }
    }
}
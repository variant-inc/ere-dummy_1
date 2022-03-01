using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using TicketType.Microservice.Core;
using Variant.TicketsShared.Messaging.Abstracts;
using Variant.TicketsShared.Messaging.Interfaces;
using Variant.TicketsShared.Messaging.PublishMessage;

namespace TicketType.Microservice.Template.Handlers
{
    [ExcludeFromCodeCoverage]
    public class EntitySqsQueueHandler : AbstractSQSHandler
    {
        private readonly IDataHandler _dataHandler;
        
        public EntitySqsQueueHandler(
            ILogger<EntitySqsQueueHandler> logger,
            IEntityApiChecklist checklist,
            IPublishMessageToSNSTopic publisher,
            IDataHandler dataHandler
        ) : base(logger, checklist, publisher)
        {
            _dataHandler = dataHandler;
            logger.LogInformation("EntitySqsQueueHandler started");
        }

        // Required by IMessageHandler
        public override async Task Init()
        {
            _logger.LogInformation("Initializing template microservice");
            
            try
            {
                await _dataHandler.ManageChecklistAsync("Outgoing",_checklist);
            }
            catch
            {
                _logger.LogError("Error Handling Data fro the Queue");
            }
            
        }
    }
}
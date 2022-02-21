using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Variant.TicketsShared.Messaging.Abstracts;
using Variant.MessageHandler.MessageHandler;
using Variant.TicketsShared.Messaging.Interfaces;

namespace TicketType.Microservice.Template.Handlers
{
    public class EntitySqsQueueHandler : AbstractSQSHandler
    {
        public EntitySqsQueueHandler(
            ILogger<IMessageHandler> logger,
            IEntityApiChecklist checklist
        ) : base (logger, checklist)
        {}

        // Required by IMessageHandler
        public override async Task Init()
        {
            if (_checklist.TractorApiReady)
            {
                _logger.LogInformation("Tractor is ready!");
            }
        }
    }
}
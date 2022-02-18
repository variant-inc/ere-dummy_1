using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Variant.TicketsShared.Messaging.Abstracts;
using Variant.MessageHandler.MessageHandler;
using Variant.TicketsShared.Messaging;
using Variant.TicketsShared.Messaging.Interfaces;

namespace TicketType.Microservice.Template.Handlers
{
    public class EntitySqsQueueHandler : AbstractSQSHandler
    {
        private readonly ILogger<IMessageHandler> _logger;
        private readonly EntityApiChecklist _checklist;

        public EntitySqsQueueHandler(
            ILogger<IMessageHandler> logger,
            EntityApiChecklist checklist
        ) : base (logger, checklist)
        {
            _logger = logger;
            _checklist = checklist;
        }

        // Required by IMessageHandler
        public async Task Init()
        {
            if (_checklist.TractorApiReady)
            {
                
            }
        }
    }
}
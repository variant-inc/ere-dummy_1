using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Variant.TicketsShared.Messaging.Abstracts;
using Variant.MessageHandler.MessageHandler;
using Variant.TicketsShared.Messaging.Interfaces;
using Variant.TicketsShared.Messaging.PublishMessage;

namespace TicketType.Microservice.Template.Handlers
{
    [ExcludeFromCodeCoverage]
    public class EntitySqsQueueHandler : AbstractSQSHandler
    {
        public EntitySqsQueueHandler(
            ILogger<IMessageHandler> logger,
            IEntityApiChecklist checklist
        ) : base(logger, checklist)
        {
            logger.LogInformation("EntitySqsQueueHandler started.");
        }

        // Required by IMessageHandler
        public override async Task Init()
        {
            _logger.LogInformation("Initializing");

            if (_checklist.TractorApiReady)
            {
                _logger.LogInformation("Tractor is ready!");
            }

            await Task.CompletedTask;
        }
    }
}
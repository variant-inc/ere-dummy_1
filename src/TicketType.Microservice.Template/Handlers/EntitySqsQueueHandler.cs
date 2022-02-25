using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Variant.TicketsShared.Messaging.Abstracts;
using Variant.MessageHandler.MessageHandler;
using Variant.TicketsShared.Messaging.Interfaces;
using Variant.TicketsShared.Messaging.Models;
using Variant.TicketsShared.Messaging.PublishMessage;

namespace TicketType.Microservice.Template.Handlers
{
    [ExcludeFromCodeCoverage]
    public class EntitySqsQueueHandler : AbstractSQSHandler
    {
        public EntitySqsQueueHandler(
            ILogger<IMessageHandler> logger,
            IEntityApiChecklist checklist,
            IPublishMessageToSNSTopic publisher,
            IOutgoingSnsTopicMetaData outgoingSnsTopicMetaData
        ) : base(logger, checklist, publisher, outgoingSnsTopicMetaData)
        {
            logger.LogInformation("EntitySqsQueueHandler started.");
        }

        // Required by IMessageHandler
        public override async Task Init()
        {
            _logger.LogInformation("Initializing");

            var exception = new ExceptionBase
            {
                Body = "UIH liuhliuh luh iluhiu"
            };

            await _publisher.PublishMessageToSNSTopicAsync(_outgoingSnsTopicMetaData.TopicName, exception);
        }
    }
}
using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moq;
using TicketType.Microservice.Template.Handlers;
using TicketType.Microservice.Template.UnitTests.Stubs;
using Variant.MessageHandler.Entities;
using Variant.MessageHandler.MessageHandler;
using Variant.TicketsShared.Messaging;
using Variant.TicketsShared.Messaging.Constants;
using Variant.TicketsShared.Messaging.Interfaces;
using Variant.TicketsShared.Messaging.Models;
using Variant.TicketsShared.Messaging.PublishMessage;
using Xunit;

namespace TicketType.Microservice.Template.UnitTests.Handlers
{
    public class EntitySqsQueueHandlerTests
    {
        private readonly Mock<ILogger<IMessageHandler>> _logger;
        private readonly Mock<IOutgoingSnsTopicMetaData> _mockOutgoingTopic;
        private readonly Mock<IPublishMessageToSNSTopic> _mockPublisher;
        private readonly IEntityApiChecklist _checklist;

        public EntitySqsQueueHandlerTests()
        {
            _logger = new Mock<ILogger<IMessageHandler>>();
            _mockOutgoingTopic = new Mock<IOutgoingSnsTopicMetaData>
            {
                Name = It.IsAny<string>()
            };
            _mockPublisher = new Mock<IPublishMessageToSNSTopic>();
            _checklist = new EntityApiChecklist();
        }

        [Fact]
        public async Task Init_Ok()
        {
            _logger.Setup(x => x.Log(
                    LogLevel.Information,
                    It.IsAny<EventId>(),
                    It.IsAny<It.IsAnyType>(),
                    It.IsAny<Exception>(),
                    (Func<It.IsAnyType, Exception, string>)It.IsAny<object>()))
                .Verifiable();
            var message = new Message
            {
                Body = JsonStub.GetGoodJson(EntityEventTypes.JOB_STARTED, EntityTypes.Tractor)
            };
            var exception = new ExceptionBase
            {
                Body = "uilh iuh liuhiu"
            };
            _mockPublisher.Setup(p => p.PublishMessageToSNSTopicAsync(It.IsAny<string>(), exception, null))
                .Verifiable();
            var handler = new EntitySqsQueueHandler(_logger.Object, _checklist, _mockPublisher.Object, _mockOutgoingTopic.Object);

            await handler.HandleEventAsync(message, It.IsAny<CancellationToken>());

        //     mockPublisher.Verify(
        // p => p.PublishMessageToSNSTopicAsync(It.IsAny<string>(), exception, null),
        //         Times.Once);
            _logger.Verify(x => x.Log(
                LogLevel.Information,
                It.IsAny<EventId>(),
                It.IsAny<It.IsAnyType>(),
                It.IsAny<Exception>(),
                (Func<It.IsAnyType, Exception, string>)It.IsAny<object>()), Times.Exactly(4));
        }
    }
}
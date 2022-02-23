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
using Xunit;

namespace TicketType.Microservice.Template.UnitTests.Handlers
{
    public class EntitySqsQueueHandlerTests
    {
        private readonly Mock<ILogger<IMessageHandler>> _logger;
        private readonly IEntityApiChecklist _checklist;

        public EntitySqsQueueHandlerTests()
        {
            _logger = new Mock<ILogger<IMessageHandler>>();
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
            var handler = new EntitySqsQueueHandler(_logger.Object, _checklist);

            await handler.HandleEventAsync(message, It.IsAny<CancellationToken>());

            _logger.Verify(x => x.Log(
                LogLevel.Information,
                It.IsAny<EventId>(),
                It.IsAny<It.IsAnyType>(),
                It.IsAny<Exception>(),
                (Func<It.IsAnyType, Exception, string>)It.IsAny<object>()), Times.Exactly(2));
        }
    }
}
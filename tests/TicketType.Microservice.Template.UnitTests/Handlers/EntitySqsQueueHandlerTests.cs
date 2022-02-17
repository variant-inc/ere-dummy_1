using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moq;
using TicketType.Microservice.Template.Handlers;
using TicketType.Microservice.Template.UnitTests.Stubs;
using Variant.MessageHandler.Entities;
using Variant.MessageHandler.MessageHandler;
using Xunit;

namespace TicketType.Microservice.Template.UnitTests.Handlers
{
    public class EntitySqsQueueHandlerTests
    {
        private readonly Mock<ILogger<IMessageHandler>> _logger;

        public EntitySqsQueueHandlerTests()
        {
            _logger = new Mock<ILogger<IMessageHandler>>();
        }

        [Fact]
        public async Task HandleEventAsyncTest_Ok()
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
                Attributes = new Dictionary<string, string>()
                {
                    {"type", "Something"}
                },
                Body = JsonStub.GetGoodJson()
            };

            var handler = new EntitySqsQueueHandler(_logger.Object);
            await handler.HandleEventAsync(message, It.IsAny<CancellationToken>());
            
            _logger.Verify(x => x.Log(
                LogLevel.Information,
                It.IsAny<EventId>(),
                It.IsAny<It.IsAnyType>(),
                It.IsAny<Exception>(),
                (Func<It.IsAnyType, Exception, string>)It.IsAny<object>()), Times.Exactly(2));
        }

        [Fact]
        public async Task HandleEventAsyncTest_WrongType()
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
                Attributes = new Dictionary<string, string>()
                {
                    {"type", "Other thing"}
                },
                Body = ""
            };

            var handler = new EntitySqsQueueHandler(_logger.Object);
            await handler.HandleEventAsync(message, It.IsAny<CancellationToken>());
            
            _logger.Verify(x => x.Log(
                LogLevel.Information,
                It.IsAny<EventId>(),
                It.IsAny<It.IsAnyType>(),
                It.IsAny<Exception>(),
                (Func<It.IsAnyType, Exception, string>)It.IsAny<object>()), Times.Once);
        }

        [Fact]
        public async Task HandleEventAsyncTest_Throws()
        {
            _logger.Setup(x => x.Log(
                LogLevel.Error,
                It.IsAny<EventId>(),
                It.IsAny<It.IsAnyType>(),
                It.IsAny<Exception>(),
                (Func<It.IsAnyType, Exception, string>)It.IsAny<object>()))
                .Verifiable();

            var message = new Message();

            var handler = new EntitySqsQueueHandler(_logger.Object);

            await Assert.ThrowsAsync<NullReferenceException>(async () => await handler.HandleEventAsync(message, It.IsAny<CancellationToken>()));

            _logger.Verify(x => x.Log(
                LogLevel.Error,
                It.IsAny<EventId>(),
                It.IsAny<It.IsAnyType>(),
                It.IsAny<Exception>(),
                (Func<It.IsAnyType, Exception, string>)It.IsAny<object>()), Times.Once);
        }
    }
}
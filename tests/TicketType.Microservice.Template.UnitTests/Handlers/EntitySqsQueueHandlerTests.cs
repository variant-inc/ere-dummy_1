using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moq;
using TicketType.Microservice.Core;
using TicketType.Microservice.Core.Interfaces;
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
        private readonly Mock<ILogger<EntitySqsQueueHandler>> _logger;
        private readonly Mock<IPublishMessageToSNSTopic> _mockPublisher;
        private readonly Mock<IProcessTickets> _mockProcessTickets;
        private readonly IEntityApiChecklist _checklist;

        public EntitySqsQueueHandlerTests()
        {
            _logger = new Mock<ILogger<EntitySqsQueueHandler>>();
            _mockPublisher = new Mock<IPublishMessageToSNSTopic>();
            _checklist = new EntityApiChecklist();
            _mockProcessTickets = new Mock<IProcessTickets>();
        }

        /*
        [Fact]
        public async Task Init_Ok()
        {
            _logger.Setup(x => x.Log(
                    LogLevel.Information,
                    It.IsAny<EventId>(),
                    It.IsAny<It.IsAnyType>(),
                    It.IsAny<System.Exception>(),
                    (Func<It.IsAnyType, System.Exception, string>)It.IsAny<object>()))
                .Verifiable();
            var message = new Message
            {
                Body = JsonStub.GetGoodJson(EntityEventTypes.JOB_STARTED, EntityTypes.Tractor)
            };
            
            _mockPublisher.Setup(p => p.PublishMessageToSNSTopicAsync(It.IsAny<string>(), It.IsAny<ExceptionBase>(), It.IsAny<Dictionary<string, string>>()))
                .Verifiable();
            
            _mockPublisher.Setup(p => p.PublishMessageToSNSTopicAsync(It.IsAny<string>(), It.IsAny<ExceptionBase>(), It.IsAny<Dictionary<string, string>>()))
                .Verifiable();
            
            var handler = new EntitySqsQueueHandler(_logger.Object, _checklist, _mockPublisher.Object,_mockProcessTickets.Object);

            await handler.HandleEventAsync(message, It.IsAny<CancellationToken>());

            handler.Verify(
                p => p.ManageChecklistAsync(It.IsAny<string>(), It.IsAny<IEntityApiChecklist>()),
                Times.Once);
            
            _logger.Verify(x => x.Log(
                LogLevel.Information,
                It.IsAny<EventId>(),
                It.IsAny<It.IsAnyType>(),
                It.IsAny<System.Exception>(),
                (Func<It.IsAnyType, System.Exception, string>)It.IsAny<object>()), Times.Exactly(4));
        }
        */
    }
}
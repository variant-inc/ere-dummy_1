using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moq;
using TicketType.Microservice.Template.Handlers;
using Variant.MessageHandler.Entities;
using Variant.MessageHandler.MessageHandler;
using Variant.TicketsShared.Messaging;
using Variant.TicketsShared.Messaging.Interfaces;
using Xunit;

namespace TicketType.Microservice.Template.UnitTests.Handlers
{
    public class EntitySqsQueueHandlerTests
    {
        private readonly Mock<ILogger<IMessageHandler>> _logger;
        private EntityApiChecklist _checklist;

        public EntitySqsQueueHandlerTests()
        {
            _logger = new Mock<ILogger<IMessageHandler>>();
            _checklist = new EntityApiChecklist();
        }
    }
}
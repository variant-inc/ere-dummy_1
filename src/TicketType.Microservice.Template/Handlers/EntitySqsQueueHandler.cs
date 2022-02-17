using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using TicketType.Microservice.Template.Models;
using TicketType.Microservice.Template.Extensions;
using Variant.MessageHandler.Entities;
using Variant.MessageHandler.MessageHandler;

namespace TicketType.Microservice.Template.Handlers
{
    public class EntitySqsQueueHandler : IMessageHandler
    {
        private readonly ILogger<IMessageHandler> _logger;

        public EntitySqsQueueHandler(ILogger<IMessageHandler> logger)
        {
            _logger = logger;
        }

        // Required by IMessageHandler
        public async Task HandleEventAsync(Message message, CancellationToken token)
        {
            _logger.LogInformation("Received SQS message");

            try
            {
                message.Attributes.TryGetValue("type", out var type);

                if (type != "Something")
                {
                    return;
                }

                await ProcessMessageAsync(message, token);
            }
            catch (Exception e)
            {
                _logger.LogError($"Message from queue processing error: {e.Message}");

                if (!e.Data.Contains("messageContext"))
                    e.Data.Add("messageContext", message);

                throw;
            }
        }

        private async Task ProcessMessageAsync(Message message, CancellationToken token)
        {
            _logger.LogInformation("Starting to process message...");
            var entityApiMessage = message.Body.Deserialize<EntityApiMessage>();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using TicketType.Microservice.Core.Helpers;
using TicketType.Microservice.Core.Interfaces;
using TicketType.Microservice.Core.Models;
using Variant.TicketsShared.Messaging.Models;
using Variant.TicketsShared.Messaging.PublishMessage;

namespace TicketType.Microservice.Core.PublishMessage
{
    public class PublishMessages : IPublishMessages
    {
        private readonly ILogger _logger;
        private readonly IPublishMessageToSNSTopic _publishMessageToSnsTopic;
        
        public PublishMessages(ILogger<PublishMessages> logger, IPublishMessageToSNSTopic publishMessageToSnsTopic)
        {
            _logger = logger;
            _publishMessageToSnsTopic = publishMessageToSnsTopic;
        }
        
        public async Task PublishMessageToSnsTopic(List<TicketMessage> tickets)
        {
            try
            {
                foreach (TicketMessage message in tickets)
                {
                    var exception = new ExceptionBase
                    {
                        Body = message.ToString()
                    };
                    await _publishMessageToSnsTopic.PublishMessageToSNSTopicAsync(GlobalVars.SnsTopicName, exception);
                }
            }
            catch
            {
                _logger.LogError("Unable to publish message to SNS Topic");
            }
        }
    }
}
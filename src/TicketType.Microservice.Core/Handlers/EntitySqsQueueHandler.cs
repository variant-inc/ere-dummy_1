using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Variant.TicketsShared.Messaging.Abstracts;
using Variant.TicketsShared.Messaging.Interfaces;
using Variant.TicketsShared.Messaging.PublishMessage;

namespace TicketType.Microservice.Core.Handlers
{
    [ExcludeFromCodeCoverage]
    public class EntitySqsQueueHandler : AbstractSQSHandler
    {
        public EntitySqsQueueHandler(
            ILogger<EntitySqsQueueHandler> logger,
            IEntityApiChecklist checklist,
            IPublishMessageToSNSTopic publisher,
            IBusinessLogic businessLogic
        ) : base(logger, checklist, publisher, businessLogic)
        {
            logger.LogInformation("EntitySqsQueueHandler started");
        }

        public override async Task Init()
        {
            _logger.LogInformation("Initializing template microservice");
            
            try
            {
                 // Batch start here
                 // See: https://github.com/variant-inc/exception-recognition-engine/blob/f/OPT-0-move-to-bulk-queue/src/Variant.Exceptions.Handler/Services/TractorToTicketConverter/TractorToTicketConverter.cs
                 // Microservice-specific code here
                 // Example:
                 // if (_checklist.TractorApiReady)
                 // {
                 //     TractorSearchParams tractorSearchParams = {}
                 //     var tractorApiData = await _tractorData.SearchTractors(tractorSearchParams);
                 //     And any other API data calls
                 // }
                 // ... Get more data, Driver & Orders, etc.
                 // _businessLogic.ProcessData(...);
                 // Batch end here
            }
            catch
            {
                _logger.LogError("Error Handling Data from the Queue");
            }

            await Task.CompletedTask;
        }
    }
}
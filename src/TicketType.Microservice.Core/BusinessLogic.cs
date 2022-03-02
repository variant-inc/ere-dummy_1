using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Variant.TicketsShared.DataSource.Driver;
using Variant.TicketsShared.DataSource.HomeTime.ResponseModels;
using Variant.TicketsShared.DataSource.Order.ResponseModels;
using Variant.TicketsShared.DataSource.Simulation.ResponseModels;
using Variant.TicketsShared.DataSource.Tractor.ResponseModels;
using Variant.TicketsShared.Messaging.Constants;
using Variant.TicketsShared.Messaging.Interfaces;

namespace TicketType.Microservice.Core
{
    [ExcludeFromCodeCoverage]
    public class BusinessLogic : IBusinessLogic
    {
        public async Task ProcessData(TractorData tractorData)
        {
            // Create exception
            // Publish to SNS
            // await _publishMessageToSnsTopic.PublishMessageToSNSTopicAsync(SNSTopicKeyNames.DEFAULT_OUTGOING_TOPIC_KEY, exception);
        }

        public async Task ProcessData(
            TractorData tractorData,
            DriverData driverData)
        {
            // Create exception
            // Publish to SNS
            // await _publishMessageToSnsTopic.PublishMessageToSNSTopicAsync(SNSTopicKeyNames.DEFAULT_OUTGOING_TOPIC_KEY, exception);
        }

        public async Task ProcessData(
            TractorData tractorData,
            DriverData driverData,
            OrderData orderData)
        {
            // Create exception
            // Publish to SNS
            // await _publishMessageToSnsTopic.PublishMessageToSNSTopicAsync(SNSTopicKeyNames.DEFAULT_OUTGOING_TOPIC_KEY, exception);
        }

        public async Task ProcessData(
            TractorData tractorData,
            DriverData driverData,
            OrderData orderData,
            HomeTimeData hometime)
        {
            // Create exception
            // Publish to SNS
            // await _publishMessageToSnsTopic.PublishMessageToSNSTopicAsync(SNSTopicKeyNames.DEFAULT_OUTGOING_TOPIC_KEY, exception);
        }

        public async Task ProcessData(
            TractorData tractorData,
            DriverData driverData,
            OrderData orderData,
            HomeTimeData hometime,
            SimulationTractorData simulated)
        {
            // Create exception
            // Publish to SNS
            // await _publishMessageToSnsTopic.PublishMessageToSNSTopicAsync(SNSTopicKeyNames.DEFAULT_OUTGOING_TOPIC_KEY, exception);
        }
    }
}
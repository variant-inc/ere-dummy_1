using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moq;
using TicketType.Microservice.Core.Exceptions;
using TicketType.Microservice.Core.Interfaces;
using TicketType.Microservice.Core.Models;
using Variant.TicketsShared.DataSource.Driver;
using Variant.TicketsShared.DataSource.Order.ResponseModels;
using Variant.TicketsShared.DataSource.Tractor.ResponseModels;
using Xunit;

namespace TicketType.Microservice.Template.UnitTests.Exception
{
    public class ProcessTicketsTests
    {
        private readonly Mock<ILogger<ProcessTickets>> _logger;
        private readonly Mock<IGetDataFromApi> _mockGetDataFromApi;
        private readonly Mock<ICreateTickets> _mockCreateTickets;
        private readonly Mock<IPublishMessages> _mockPublishMessages;
        
        public ProcessTicketsTests()
        {
            _logger = new Mock<ILogger<ProcessTickets>>();
            _mockCreateTickets = new Mock<ICreateTickets>();
            _mockGetDataFromApi = new Mock<IGetDataFromApi>();
            _mockPublishMessages = new Mock<IPublishMessages>();;
        }
        
        [Fact]
        public async Task ProcessDriverTickets_Success()
        {
            var driverData = new List<DriverData>(){
                new DriverData {
                },
                new DriverData {
                }
            };
            
            var tickets = new List<TicketMessage>(){
                new TicketMessage {
                },
                new TicketMessage {
                }
            };
            var topicName = "dummy Name";
            
            _mockGetDataFromApi.Setup(g => g.GetDataFromDriverApi())
                .ReturnsAsync(driverData);
            _mockCreateTickets.Setup(c => c.CreateTicketsFromDriverData(driverData))
                .Returns(tickets);

            var process = new ProcessTickets(_logger.Object, _mockCreateTickets.Object,
                                                _mockGetDataFromApi.Object, _mockPublishMessages.Object);

            await process.ProcessDriverTickets(topicName);
            
            _mockPublishMessages.Verify(
                p => p.PublishMessageToSnsTopic(topicName,tickets),
                Times.Once);
        }
        
        [Fact]
        public async Task ProcessTractorTickets_Success()
        {
            var tractorData = new List<TractorData>(){
                new TractorData {
                },
                new TractorData {
                }
            };
            
            var tickets = new List<TicketMessage>(){
                new TicketMessage {
                },
                new TicketMessage {
                }
            };
            var topicName = "dummy Name";
            
            _mockGetDataFromApi.Setup(g => g.GetDataFromTractorApi())
                .ReturnsAsync(tractorData);
            _mockCreateTickets.Setup(c => c.CreateTicketsFromTractorData(tractorData))
                .Returns(tickets);

            var process = new ProcessTickets(_logger.Object, _mockCreateTickets.Object,
                _mockGetDataFromApi.Object, _mockPublishMessages.Object);

            await process.ProcessTractorTickets(topicName);
            
            _mockPublishMessages.Verify(
                p => p.PublishMessageToSnsTopic(topicName, tickets),
                Times.Once);
        }
        
        [Fact]
        public async Task ProcessOrderTickets_Success()
        {
            var orderData = new List<OrderData>(){
                new OrderData {
                },
                new OrderData {
                }
            };
            
            var tickets = new List<TicketMessage>(){
                new TicketMessage {
                },
                new TicketMessage {
                }
            };
            var topicName = "dummy Name";
            
            _mockGetDataFromApi.Setup(g => g.GetDataFromOrderApi())
                .ReturnsAsync(orderData);
            _mockCreateTickets.Setup(c => c.CreateTicketsFromOrderData(orderData))
                .Returns(tickets);

            var process = new ProcessTickets(_logger.Object, _mockCreateTickets.Object,
                _mockGetDataFromApi.Object, _mockPublishMessages.Object);

            await process.ProcessOrderTickets(topicName);
            
            _mockPublishMessages.Verify(
                p => p.PublishMessageToSnsTopic(topicName, tickets),
                Times.Once);
        }
    }
}
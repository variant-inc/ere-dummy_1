using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketType.Microservice.Template.Infrastructure;
using TicketType.Microservice.Template.Services;
using Variant.TicketsShared.DataSource.Infrastructure;
using Xunit;

namespace TicketType.Microservice.Template.UnitTests.Services
{
    public class ErrorHandlerTests
    {
        [Fact]
        public void HandleBatchProcessingException_ShouldHandleDataSourceException()
        {
            //should throw ex, should log proper message
            var uut = GetUUT(out var logger);

            Assert.Throws<BatchProcessFailedException>(() => uut.HandleBatchProcessingException(new DataSourceException("message", "original message")));
        }

        [Fact]
        public void HandleExceptionItemGenerationIssue_ShouldNotPropagateException()
        {
            var uut = GetUUT(out var logger);
            uut.HandleExceptionItemGenerationIssue(new { }, new ApplicationException());
        }

        private static GeneratorErrorHandler GetUUT(out Mock<ILogger<GeneratorErrorHandler>> logger)
        {
            logger = new Mock<ILogger<GeneratorErrorHandler>>();
            return new GeneratorErrorHandler(logger.Object);
        }
    }
}

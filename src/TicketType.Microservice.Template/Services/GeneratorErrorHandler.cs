using Microsoft.Extensions.Logging;
using System;
using System.Text.Json;
using TicketType.Microservice.Template.Infrastructure;
using Variant.TicketsShared.DataSource.Infrastructure;

namespace TicketType.Microservice.Template.Services
{
    public class GeneratorErrorHandler
    {
        private readonly ILogger<GeneratorErrorHandler> _logger;

        public GeneratorErrorHandler(ILogger<GeneratorErrorHandler> logger)
        {
            _logger = logger;
        }

        //Use at the batch processing stage
        public void HandleBatchProcessingException(Exception ex)
        {
            if (ex is DataSourceException)
            {
                HandleDataSourceIssue(ex as DataSourceException);
            }

            //handle trigger sns issue

            //handle exception publish issue

            _logger.LogError(ex.Message);
            throw new BatchProcessFailedException();
        }

        //Use at single exception generation stage
        public void HandleExceptionItemGenerationIssue(object inputData, Exception ex)
        {
            _logger.LogError(ex, $"Failed to generate exception for data reason: {ex.Message}, input data: {JsonSerializer.Serialize(inputData)} ");
        }

        private void HandleDataSourceIssue(DataSourceException ex)
        {
            _logger.LogError(ex, $"Ticket generation process failed, reason: data source issue: {ex.Message}, issue description: {ex.OriginalMessage} ");
            throw new BatchProcessFailedException();
        }
    }
}

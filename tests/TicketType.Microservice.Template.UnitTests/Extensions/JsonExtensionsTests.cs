using System;
using TicketType.Microservice.Template.Extensions;
using Xunit;

namespace TicketType.Microservice.Template.UnitTests.Extensions
{
    public class JsonExtensionsTests
    {
        [Fact]
        public void SnakeCaseSettingsTest_SerializingDate()
        {
            const string dummyDateString = "2002-05-26T01:23:45Z";
            var parsedDate = DateTime.Parse(dummyDateString);

            var json = parsedDate.Serialize(JsonExtensions.SnakeCaseSettings);
            
            Assert.Equal("\"" + dummyDateString + "\"" , json);
        }
    }
}
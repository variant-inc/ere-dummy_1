using System;
using Newtonsoft.Json;
using TicketType.Microservice.Template.Extensions;
using TicketType.Microservice.Template.Models;
using TicketType.Microservice.Template.UnitTests.Stubs;
using Variant.MessageHandler.Entities;
using Xunit;

namespace TicketType.Microservice.Template.UnitTests.Extensions
{
    public class JsonExtensionsTests
    {
        private string _defaultMessageBody = "Is the loneliest number";

        [Fact]
        public void DeserializeTest_DefaultSettings_Ok()
        {
            var message = new Message
            {
                Body = JsonStub.GetGoodJson()
            };
            var result = message.Body.Deserialize<EntityApiMessage>();

            Assert.IsType<EntityApiMessage>(result);
            Assert.StrictEqual(_defaultMessageBody, result.Message);
        }

        [Fact]
        public void DeserializeTest_SnakeCaseSettings_Ok()
        {
            var message = new Message
            {
                Body = JsonStub.GetGoodJson()
            };
            var result = message.Body.Deserialize<EntityApiMessage>(JsonExtensions.SnakeCaseSettings);
        
            Assert.IsType<EntityApiMessage>(result);
            Assert.StrictEqual(_defaultMessageBody, result.Message);
        }

        [Fact]
        public void DeserializeTest_DefaultSettings_Throws()
        {
            var message = new Message
            {
                Body = JsonStub.GetBadJson()
            };
        
            Assert.Throws<JsonReaderException>(() => message.Body.Deserialize<EntityApiMessage>());
        }

        [Fact]
        public void DefaultSettingsTest_SerializingDate()
        {
            const string dummyDateString = "2002-05-26T01:23:45Z";
            var parsedDate = DateTime.Parse(dummyDateString);
        
            var json = parsedDate.Serialize();
            
            Assert.Equal("\"2002-05-25T21:23:45-04:00\"" , json);
        }

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
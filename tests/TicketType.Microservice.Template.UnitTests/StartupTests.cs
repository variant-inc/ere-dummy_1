using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TicketType.Microservice.Template.Handlers;
using Xunit;

namespace TicketType.Microservice.Template.UnitTests
{
    public class StartupTests
    {
        private static readonly Dictionary<string, string> ConfigurationSettings = new Dictionary<string, string>
        {
            ["AWS:REGION"] = "nomatter",
            ["AWS:SERVICEURL"] = "http://aws-dude.com/",
            ["AWS:USEHTTP"] = "true",
            ["SQS:QUEUEURL"] = "https://queue-baby.net",
            ["HealthChecks:Enabled"] = "true",
        };

        [Fact]
        public void CanResolveExpectedDependencies()
        {
            var dependency = typeof(EntitySqsQueueHandler);
            var hostBuilderContext = new HostBuilderContext(new Dictionary<object, object>())
            {
                Configuration = new ConfigurationBuilder()
                    .AddInMemoryCollection(ConfigurationSettings)
                    .Build()
            };

            var services = new ServiceCollection();
            Startup.ConfigureServices(hostBuilderContext, services);
            var provider = services.BuildServiceProvider();
            var instance = ActivatorUtilities.CreateInstance(provider, dependency);

            Assert.IsType<EntitySqsQueueHandler>(instance);
        }
    }
}
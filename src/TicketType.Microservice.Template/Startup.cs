using Epsagon.Dotnet.Instrumentation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TicketType.Microservice.Template.Handlers;
using TicketType.Microservice.Template.Modules;
using Variant.MessageHandler.DependencyInjection;
using Variant.MessageHandler.Epsagon;
using Variant.MessageHandler.HealthChecks.HealthChecks.AtCapacityWithinWindow;
using Variant.MessageHandler.HealthChecks.HealthChecks.AverageMessageProcessingDuration;
using Variant.MessageHandler.HealthChecks.HealthChecks.FailedMessageRatio;
using Variant.MessageHandler.HealthChecks.HealthChecks.TimeSinceLastBatchReceive;
using Variant.MessageHandler.Sqs.DependencyInjection;

namespace TicketType.Microservice.Template
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            EpsagonBootstrap.Bootstrap();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public static void ConfigureServices(HostBuilderContext hostContext, IServiceCollection services)
        {
            var config = hostContext.Configuration;
            var healthCheckConfig = config.GetSection("HealthChecks");
            var areHealthChecksEnabled = healthCheckConfig.GetValue<bool>("Enabled");
            // var healthCheckRunnerConfig = healthCheckConfig.GetSection("Runner");
            // var consoleHealthCheckServerConfig = healthCheckConfig.GetSection("Server");

            void ConfigureMessageConfigurator(IMessageHandlerConfigurator configurator)
            {
                configurator.AddEpsagonTracing();

                if (areHealthChecksEnabled)
                {
                    configurator
                        .AddAverageMessageDurationHealthCheck(opts => opts.ProvideDetailedInformation = false)
                        .AddAtCapacityWithinWindowHealthCheck(opts => opts.ProvideDetailedInformation = false)
                        .AddFailedMessageRatioHealthCheck(opts => opts.ProvideDetailedInformation = false)
                        .AddTimeSinceLastBatchReceiveHealthCheck(opts => opts.ProvideDetailedInformation = false);
                }
            }

            services.AddFeatureFlags(config);
            services.AddSqsMessageRetriever<EntitySqsQueueHandler>(
                config.GetAWSOptions(),
                config.GetSection("SQS"),
                ConfigureMessageConfigurator);

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // if (env.IsDevelopment())
            // {
            //     app.UseDeveloperExceptionPage();
            //     app.UseSwagger();
            //     app.UseSwaggerUI(c =>
            //         c.SwaggerEndpoint("/swagger/v1/swagger.json", "TicketType.Microservice.Template v1"));
            // }
            //
            // app.UseHttpsRedirection();
            //
            // app.UseRouting();
            //
            // app.UseAuthorization();
            //
            // app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}
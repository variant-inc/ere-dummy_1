using System;
using System.Diagnostics.CodeAnalysis;
using Epsagon.Dotnet.Instrumentation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TicketType.Microservice.Template.Handlers;
using TicketType.Microservice.Template.Infrastructure;
using Variant.TicketsShared.DataSource.Infrastructure;
using Variant.TicketsShared.Messaging.DependencyInjection;

namespace TicketType.Microservice.Template
{
    [ExcludeFromCodeCoverage]
    public static class Startup
    {
        static Startup()
        {
            EpsagonBootstrap.Bootstrap();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public static void ConfigureServices(HostBuilderContext hostContext, IServiceCollection services)
        {
            var config = hostContext.Configuration;

            services.AddAutoMapper(cfg => cfg.AddMaps(AppDomain.CurrentDomain.GetAssemblies()));

            services.AddDataSources(config);

            services.AddMessagingServices<EntitySqsQueueHandler>(config);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        // public static void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        // {
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
        // }
    }
}
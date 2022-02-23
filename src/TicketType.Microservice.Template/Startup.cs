using System.Diagnostics.CodeAnalysis;
using Epsagon.Dotnet.Instrumentation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TicketType.Microservice.Template.Handlers;
using Variant.TicketsShared.Messaging.DependencyInjection;

namespace TicketType.Microservice.Template
{
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
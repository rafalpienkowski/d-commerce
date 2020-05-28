using System;
using Coravel;
using Framework;
using GreenPipes;
using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Shipping.BusinessCustomers;
using Shipping.Framework;
using Shipping.Messages.Commands;

namespace Shipping
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddDbContext<ShippingOrderContext>();
            services.AddScoped<ShippingOrderContext>();
            
            services.AddScoped<IShippingOrderRepository, ShippingOrderRepository>();
            services.AddScoped<IShippingAddressesRepository, FakeShippingAddressesRepository>();
            services.AddScoped<IShippingProvider, FakeShippingProvider>();
            
            services.AddMassTransit(x =>
            {
                x.AddConsumer<CreateShippingConsumer>();
                x.AddConsumer<PaymentAcceptedConsumer>();
                x.AddBus(context => Bus.Factory.CreateUsingRabbitMq(cfg =>
                {
                    cfg.Host("rabbitmq://localhost");
                    cfg.ReceiveEndpoint(typeof(CreateShipping).GetQueueName(), ep =>
                    {
                        ep.ConfigureConsumer<CreateShippingConsumer>(context);
                    });
                    cfg.ReceiveEndpoint("shipping_payment_accepted", ep =>
                    {
                        ep.UseMessageRetry(r => r.Interval(3, TimeSpan.FromSeconds(15)));
                        ep.ConfigureConsumer<PaymentAcceptedConsumer>(context);
                    });
                }));
            });

            services.AddScoped<ICommandBus, MassTransitCommandBus>();
            services.AddScoped<IEventBus, MassTransitEventBus>();

            services.AddScheduler();
            services.AddTransient<EventDispatcher>();
            
            services.AddMassTransitHostedService();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
            
            
        }
    }
}
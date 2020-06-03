using Billing.Messages.Commands;
using Billing.Payments;
using Framework;
using Framework.Extensions;
using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Billing
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
            services.AddTracing();
            services.AddDbContext<PaymentDetailsContext>();
            services.AddScoped<PaymentDetailsContext>();

            services.AddScoped<ICardDetailsRepository, FakeCardDetailsRepository>();
            // services.AddScoped<IPaymentProvider, FakeNotSoReliablePaymentProvider>();
            services.AddScoped<IPaymentProvider, FakePaymentProvider>();
            services.AddScoped<IPaymentDetailsRepository, PaymentDetailsRepository>();
            
            services.AddMassTransit(x =>
            {
                x.AddConsumer<OrderCreatedConsumer>();
                x.AddConsumer<ChargePaymentConsumer>();
                x.AddBus(context => Bus.Factory.CreateUsingRabbitMq(cfg =>
                {
                    cfg.ConfigureHost();
                    cfg.ReceiveEndpoint("billing_order_created", ep =>
                    {
                        ep.ConfigureConsumer<OrderCreatedConsumer>(context);
                    });
                    cfg.ReceiveEndpoint(typeof(ChargePayment).GetQueueName(), ep =>
                    {
                        //ep.UseMessageRetry(r => r.Interval(3, TimeSpan.FromSeconds(1)));
                        ep.ConfigureConsumer<ChargePaymentConsumer>(context);
                    });
                }));
            });
            
            services.AddScoped<ICommandBus, MassTransitCommandBus>();
            services.AddScoped<IEventBus, MassTransitEventBus>();
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
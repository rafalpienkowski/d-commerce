using Framework;
using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Web.Pages.Orders;

namespace Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddServerSideBlazor();

            services.AddDbContext<OrdersContext>();
            services.AddScoped<OrdersContext>();
            services.AddScoped<IOrdersRepository, OrdersRepository>();
            
            services.AddMassTransit(x =>
            {
                x.AddConsumer<OrderPlacedConsumer>();
                x.AddConsumer<OrderCreatedConsumer>();
                x.AddConsumer<PaymentAcceptedConsumer>();
                x.AddConsumer<PaymentRejectedConsumer>();
                x.AddConsumer<ShippingArrangedConsumer>();
                x.AddBus(context => Bus.Factory.CreateUsingRabbitMq(cfg =>
                {
                    cfg.Host("rabbitmq://localhost");
                    cfg.ReceiveEndpoint("web_order_placed", ep =>
                    {
                        ep.ConfigureConsumer<OrderPlacedConsumer>(context);
                    });
                    cfg.ReceiveEndpoint("web_order_created", ep =>
                    {
                        ep.ConfigureConsumer<OrderCreatedConsumer>(context);
                    });
                    cfg.ReceiveEndpoint("web_payment_accepted", ep =>
                    {
                        ep.ConfigureConsumer<PaymentAcceptedConsumer>(context);
                    });
                    cfg.ReceiveEndpoint("web_payment_rejected", ep =>
                    {
                        ep.ConfigureConsumer<PaymentRejectedConsumer>(context);
                    });
                    cfg.ReceiveEndpoint("web_shipping_arranged", ep =>
                    {
                        ep.ConfigureConsumer<ShippingArrangedConsumer>(context);
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
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}
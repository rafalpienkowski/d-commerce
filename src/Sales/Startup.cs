using Framework;
using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Sales.Messages.Commands;
using Sales.Orders;

namespace Sales
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

            services.AddDbContext<OrdersDbContext>();
            services.AddScoped<OrdersDbContext>();
            
            services.AddScoped<ICostCalculator, SillyCostCalculator>();
            services.AddScoped<IOrdersRepository, OrdersRepository>();
            services.AddScoped<IOrderNumberGenerator, SillyOrderNumberGenerator>();
            
            services.AddMassTransit(x =>
            {
                x.AddConsumer<PlaceOrderCustomer>();
                x.AddBus(context => Bus.Factory.CreateUsingRabbitMq(cfg =>
                {
                    cfg.Host("rabbitmq://localhost");
                    cfg.ReceiveEndpoint(typeof(PlaceOrder).GetQueueName(), ep =>
                    {
                        ep.ConfigureConsumer<PlaceOrderCustomer>(context);
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
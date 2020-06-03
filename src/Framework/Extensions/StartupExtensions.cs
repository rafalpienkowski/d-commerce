using System.Reflection;
using Jaeger;
using Jaeger.Samplers;
using MassTransit.RabbitMqTransport;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using OpenTracing;
using OpenTracing.Util;
using MassTransit.OpenTracing;
using MassTransit;

namespace Framework.Extensions
{
    public static class StartupExtensions
    {
        public static void AddTracing(this IServiceCollection services)
        {
            services.AddSingleton<ITracer>(serviceProvider =>  
            {  
                string serviceName = Assembly.GetEntryAssembly().GetName().Name;
                ILoggerFactory loggerFactory = serviceProvider.GetRequiredService<ILoggerFactory>();
                ISampler sampler = new ConstSampler(sample: true);
                ITracer tracer = new Tracer.Builder(serviceName)
                    .WithLoggerFactory(loggerFactory)
                    .WithSampler(sampler)
                    .Build();
                    
                GlobalTracer.Register(tracer);
                return tracer;
            });
            services.AddOpenTracing();
        }

        public static void ConfigureHost(this IRabbitMqBusFactoryConfigurator cfg)
        {
            cfg.Host("rabbitmq://localhost");
            cfg.PropagateOpenTracingContext();
        }
    }
}
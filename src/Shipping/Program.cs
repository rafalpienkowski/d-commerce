using Coravel;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Shipping.Framework;

namespace Shipping
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
                
            host.Services.UseScheduler(scheduler => {
                scheduler
                    .Schedule<EventDispatcher>()
                    .EveryFiveSeconds();
            });
            
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
    }
}
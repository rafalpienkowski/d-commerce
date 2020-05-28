using System.Threading.Tasks;
using MassTransit;

namespace Framework
{
    public class MassTransitEventBus : IEventBus
    {
        private readonly IPublishEndpoint _publishEndpoint;

        public MassTransitEventBus(IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint;
        }

        public Task Publish<T>(T message) where T : class => _publishEndpoint.Publish(message);

        public Task Publish(object message) => _publishEndpoint.Publish(message);
    }
}
using System;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Coravel.Invocable;
using Framework;
using Shipping.BusinessCustomers;

namespace Shipping.Framework
{
    public class EventDispatcher : IInvocable
    {
        private readonly ShippingOrderContext _dbContext;
        private readonly IEventBus _eventBus;

        public EventDispatcher(ShippingOrderContext dbContext, IEventBus eventBus)
        {
            _dbContext = dbContext;
            _eventBus = eventBus;
        }

        public async Task Invoke()
        {
            if (_dbContext.PersistedEvents.Any(pe => !pe.Processed))
            {

                foreach (var persistedEvent in _dbContext.PersistedEvents.Where(pe => !pe.Processed))
                {
                    Console.WriteLine($"Sending event: {JsonSerializer.Serialize(persistedEvent)}");
                    var eventType = GetEventType(persistedEvent.Type);
                    var @event = JsonSerializer.Deserialize(persistedEvent.Body, eventType);

                    await _eventBus.Publish(@event);
                    persistedEvent.Processed = true;
                }
                await _dbContext.SaveChangesAsync();
            }
        }

        private static Type GetEventType(string eventType)
        {
            var type = Type.GetType(eventType);
            if (type == typeof(ShippingArranged))
            {
                return typeof(Shipping.Messages.Events.ShippingArranged);
            }
            
            throw new ArgumentOutOfRangeException($"Unsupported event type: {nameof(type)}");
        }
    }
}
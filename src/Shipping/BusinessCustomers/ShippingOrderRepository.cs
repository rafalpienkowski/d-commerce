using System;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Shipping.Framework;

namespace Shipping.BusinessCustomers
{
    public class ShippingOrderRepository : IShippingOrderRepository
    {
        private readonly ShippingOrderContext _dbContext;

        public ShippingOrderRepository(ShippingOrderContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<ShippingOrder> Get(Guid orderId) =>
            _dbContext.ShippingOrders.FirstOrDefaultAsync(so => so.OrderId == orderId);

        public async Task Add(ShippingOrder shippingOrder)
        {
            await _dbContext.ShippingOrders.AddAsync(shippingOrder);
        }

        public async Task SaveChanges()
        {
            var modifiedEntities = _dbContext.ChangeTracker.Entries()
                .Where(x => x.State != EntityState.Unchanged && x.Entity is Entity)
                .ToList();

            foreach (var @event in modifiedEntities.Select(modifiedEntity => modifiedEntity.Entity).OfType<Entity>()
                .SelectMany(domainEntity => domainEntity.GetPendingEvents()))
            {
                
                var body = JsonSerializer.Serialize(@event, @event.GetType());
                
                await _dbContext.PersistedEvents.AddAsync(new PersistedEvent
                {
                    Id = @event.Id,
                    TimeStamp = @event.TimeStamp,
                    Type = @event.GetType().ToString(),
                    Body = body
                });
            }

            await _dbContext.SaveChangesAsync();
        }
    }
}
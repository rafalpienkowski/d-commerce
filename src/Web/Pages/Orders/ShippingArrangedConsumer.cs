using System;
using System.Threading.Tasks;
using MassTransit;
using Shipping.Messages.Events;

namespace Web.Pages.Orders
{
    public class ShippingArrangedConsumer : IConsumer<ShippingArranged>
    {
        private readonly IOrdersRepository _ordersRepository;
        private readonly OrdersContext _dbContext;

        public ShippingArrangedConsumer(IOrdersRepository ordersRepository, OrdersContext dbContext)
        {
            _ordersRepository = ordersRepository;
            _dbContext = dbContext;
        }

        public async Task Consume(ConsumeContext<ShippingArranged> context)
        {
            var order = await _ordersRepository.Get(context.Message.OrderId);
            order.Status = "Shipping arranged";
            order.LastUpdate = DateTime.UtcNow;

            await _dbContext.SaveChangesAsync();
        }
    }
}
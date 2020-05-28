using System;
using System.Threading.Tasks;
using MassTransit;
using Sales.Messages.Events;

namespace Web.Pages.Orders
{
    public class OrderCreatedConsumer : IConsumer<OrderCreated>
    {
        private readonly IOrdersRepository _ordersRepository;
        private readonly OrdersContext _dbContext;

        public OrderCreatedConsumer(IOrdersRepository ordersRepository, OrdersContext dbContext)
        {
            _ordersRepository = ordersRepository;
            _dbContext = dbContext;
        }
        
        public async Task Consume(ConsumeContext<OrderCreated> context)
        {
            var order = await _ordersRepository.Get(context.Message.OrderId);
            if (order == null)
            {
                throw new ArgumentNullException(nameof(context.Message.OrderId), "No such order with given id");
            }
            
            order.Status = "Created";
            order.LastUpdate = DateTime.UtcNow;
            order.Number = context.Message.Number;

            await _dbContext.SaveChangesAsync();
        }
    }
}
using System.Threading.Tasks;
using MassTransit;
using Sales.Messages.Events;

namespace Web.Pages.Orders
{
    public class OrderPlacedConsumer : IConsumer<OrderPlaced>
    {
        private readonly IOrdersRepository _ordersRepository;
        private readonly OrdersContext _dbContext;

        public OrderPlacedConsumer(IOrdersRepository ordersRepository, OrdersContext dbContext)
        {
            _ordersRepository = ordersRepository;
            _dbContext = dbContext;
        }

        public async Task Consume(ConsumeContext<OrderPlaced> context)
        {
            await _ordersRepository.Add(new Order
            {
                Id = context.Message.OrderId,
                Status = "Placed",
                Number = "",
                LastUpdate = context.Message.TimeStamp,
                UserId =  context.Message.UserId
            });

            await _dbContext.SaveChangesAsync();
        }
    }
}
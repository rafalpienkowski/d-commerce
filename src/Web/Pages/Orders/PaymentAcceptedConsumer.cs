using System;
using System.Threading.Tasks;
using Billing.Messages.Events;
using MassTransit;

namespace Web.Pages.Orders
{
    public class PaymentAcceptedConsumer : IConsumer<PaymentAccepted>
    {
        private readonly IOrdersRepository _ordersRepository;
        private readonly OrdersContext _dbContext;

        public PaymentAcceptedConsumer(IOrdersRepository ordersRepository, OrdersContext dbContext)
        {
            _ordersRepository = ordersRepository;
            _dbContext = dbContext;
        }

        public async Task Consume(ConsumeContext<PaymentAccepted> context)
        {
            var order = await _ordersRepository.Get(context.Message.OrderId);
            order.Status = "Payment accepted";
            order.LastUpdate = DateTime.UtcNow;

            await _dbContext.SaveChangesAsync();
        }
    }
}
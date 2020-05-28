using System;
using System.Threading.Tasks;
using Billing.Messages.Events;
using MassTransit;

namespace Web.Pages.Orders
{
    public class PaymentRejectedConsumer : IConsumer<PaymentRejected>
    {
        private readonly IOrdersRepository _ordersRepository;
        private readonly OrdersContext _dbContext;

        public PaymentRejectedConsumer(IOrdersRepository ordersRepository, OrdersContext dbContext)
        {
            _ordersRepository = ordersRepository;
            _dbContext = dbContext;
        }

        public async Task Consume(ConsumeContext<PaymentRejected> context)
        {
            var order = await _ordersRepository.Get(context.Message.OrderId);
            order.Status = "Payment rejected";
            order.LastUpdate = DateTime.UtcNow;

            await _dbContext.SaveChangesAsync();
        }
    }
}
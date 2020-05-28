using System;
using System.Text.Json;
using System.Threading.Tasks;
using Billing.Messages.Commands;
using Framework;
using MassTransit;
using Sales.Messages.Events;

namespace Billing.Payments
{
    public class OrderCreatedConsumer : IConsumer<OrderCreated>
    {
        private readonly ICardDetailsRepository _cardDetailsRepository;
        private readonly IPaymentDetailsRepository _paymentDetailsRepository;
        private readonly ICommandBus _commandBus;
        private readonly PaymentDetailsContext _dbContext;

        public OrderCreatedConsumer(ICardDetailsRepository cardDetailsRepository,
            IPaymentDetailsRepository paymentDetailsRepository, ICommandBus commandBus, PaymentDetailsContext dbContext)
        {
            _cardDetailsRepository = cardDetailsRepository;
            _paymentDetailsRepository = paymentDetailsRepository;
            _commandBus = commandBus;
            _dbContext = dbContext;
        }

        public async Task Consume(ConsumeContext<OrderCreated> context)
        {
            Console.WriteLine($"Message received: {JsonSerializer.Serialize(context.Message)}");

            var cardDetails = _cardDetailsRepository.GetByUserId(context.Message.UserId);

            await _paymentDetailsRepository.Add(new PaymentDetails
            {
                Amount = context.Message.Amount,
                Status = PaymentStatus.Pending,
                CardNumber = cardDetails.Number,
                OrderId = context.Message.OrderId,
                TimeStamp = DateTime.UtcNow
            });

            await _dbContext.SaveChangesAsync();

            var chargePayment = new ChargePayment
            {
                OrderId = context.Message.OrderId
            };

            await _commandBus.Send(chargePayment);
        }
    }
}
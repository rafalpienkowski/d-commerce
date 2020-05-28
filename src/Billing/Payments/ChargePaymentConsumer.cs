using System;
using System.Text.Json;
using System.Threading.Tasks;
using Billing.Messages.Commands;
using Billing.Messages.Events;
using Framework;
using MassTransit;

namespace Billing.Payments
{
    public class ChargePaymentConsumer : IConsumer<ChargePayment>
    {
        private readonly IEventBus _eventBus;
        private readonly IPaymentProvider _paymentProvider;
        private readonly IPaymentDetailsRepository _paymentDetailsRepository;
        private readonly PaymentDetailsContext _context;

        public ChargePaymentConsumer(IEventBus eventBus, IPaymentProvider paymentProvider,
            IPaymentDetailsRepository paymentDetailsRepository, PaymentDetailsContext context)
        {
            _eventBus = eventBus;
            _paymentProvider = paymentProvider;
            _paymentDetailsRepository = paymentDetailsRepository;
            _context = context;
        }

        public async Task Consume(ConsumeContext<ChargePayment> context)
        {
            Console.WriteLine($"Message received: {JsonSerializer.Serialize(context.Message)}");
            
            var paymentDetails = await _paymentDetailsRepository.GetByOrderId(context.Message.OrderId);
            var paymentConfirmation = _paymentProvider.ChargeCreditCard(paymentDetails.CardNumber, paymentDetails.Amount);
            
            paymentDetails.Status = paymentConfirmation.Succeed ? PaymentStatus.Accepted : PaymentStatus.Rejected;
            await _context.SaveChangesAsync();

            if (paymentConfirmation.Succeed)
            {
                await _eventBus.Publish(new PaymentAccepted
                {
                    OrderId = context.Message.OrderId
                });
            }
            else
            {
                await _eventBus.Publish(new PaymentRejected
                {
                    OrderId = context.Message.OrderId
                });
            }
        }
    }
}
using System;
using System.Text.Json;
using System.Threading.Tasks;
using Billing.Messages.Events;
using MassTransit;

namespace Shipping.BusinessCustomers
{
    public class PaymentAcceptedConsumer : IConsumer<PaymentAccepted>
    {
        private readonly IShippingAddressesRepository _shippingAddressesRepository;
        private readonly IShippingOrderRepository _shippingOrderRepository;
        private readonly IShippingProvider _shippingProvider;

        public PaymentAcceptedConsumer(IShippingAddressesRepository shippingAddressesRepository,
            IShippingProvider shippingProvider, IShippingOrderRepository shippingOrderRepository)
        {
            _shippingAddressesRepository = shippingAddressesRepository;
            _shippingProvider = shippingProvider;
            _shippingOrderRepository = shippingOrderRepository;
        }

        public async Task Consume(ConsumeContext<PaymentAccepted> context)
        {
            Console.WriteLine($"Message received: {JsonSerializer.Serialize(context.Message)}");
            
            var shippingOrder = await _shippingOrderRepository.Get(context.Message.OrderId);
            if (shippingOrder == null)
            {
                throw new ArgumentNullException(nameof(context.Message.OrderId));
            }
            var address = _shippingAddressesRepository.GetCustomerAddress(context.Message.OrderId);
            var confirmation = _shippingProvider.ArrangeShippingFor(address.Address, context.Message.OrderId.ToString());

            shippingOrder.UpdateShippingStatus(confirmation);

            await _shippingOrderRepository.SaveChanges();
        }
    }
}
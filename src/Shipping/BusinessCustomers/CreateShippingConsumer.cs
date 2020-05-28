using System;
using System.Text.Json;
using System.Threading.Tasks;
using MassTransit;
using Shipping.Messages.Commands;

namespace Shipping.BusinessCustomers
{
    public class CreateShippingConsumer : IConsumer<CreateShipping>
    {
        private readonly IShippingOrderRepository _shippingOrderRepository;

        public CreateShippingConsumer(IShippingOrderRepository shippingOrderRepository)
        {
            _shippingOrderRepository = shippingOrderRepository;
        }

        public async Task Consume(ConsumeContext<CreateShipping> context)
        {
            Console.WriteLine($"Message received: {JsonSerializer.Serialize(context.Message)}");
            
            var shippingOrder = ShippingOrder.Create(context.Message.UserId, context.Message.OrderId,
                context.Message.ShippingTypeId);
            
            await _shippingOrderRepository.Add(shippingOrder);
            await _shippingOrderRepository.SaveChanges();
        }
    }
}
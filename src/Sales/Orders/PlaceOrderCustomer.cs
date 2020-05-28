using System;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Framework;
using MassTransit;
using Sales.Messages.Commands;
using Sales.Messages.Events;
using Shipping.Messages.Commands;

namespace Sales.Orders
{
    public class PlaceOrderCustomer : IConsumer<PlaceOrder>
    {
        private readonly IEventBus _eventBus;
        private readonly ICommandBus _commandBus;
        private readonly ICostCalculator _costCalculator;
        private readonly IOrderNumberGenerator _orderNumberGenerator;
        private readonly IOrdersRepository _ordersRepository;
        private readonly OrdersDbContext _dbContext;

        public PlaceOrderCustomer(IEventBus eventBus, ICostCalculator costCalculator,
            IOrderNumberGenerator orderNumberGenerator, IOrdersRepository ordersRepository, OrdersDbContext dbContext,
            ICommandBus commandBus)
        {
            _eventBus = eventBus;
            _costCalculator = costCalculator;
            _orderNumberGenerator = orderNumberGenerator;
            _ordersRepository = ordersRepository;
            _dbContext = dbContext;
            _commandBus = commandBus;
        }

        public async Task Consume(ConsumeContext<PlaceOrder> context)
        {
            Console.WriteLine($"Message received: {JsonSerializer.Serialize(context.Message)}");

            var orderNumber = await _orderNumberGenerator.GetNumber(context.Message.UserId);

            var order = new Order
            {
                Id = context.Message.Id,
                Number = orderNumber,
                ProductIds = string.Join(",", context.Message.ProductIds),
                ShippingTypeId = context.Message.ShippingTypeId,
                UserId = context.Message.UserId,
                TimeStamp = DateTime.UtcNow,
                Amount = _costCalculator.CostForProducts(context.Message.ProductIds)
            };

            await _ordersRepository.Add(order);
            await _dbContext.SaveChangesAsync();

            //Some expensive calculation
            Thread.Sleep(10000);

            await _eventBus.Publish(new OrderCreated
            {
                OrderId = order.Id,
                Number = order.Number,
                ProductIds = order.ProductIds.Split(","),
                ShippingTypeId = order.ShippingTypeId,
                UserId = order.UserId,
                TimeStamp = order.TimeStamp,
                Amount = order.Amount
            });

            await _commandBus.Send(new CreateShipping
            {
                OrderId = order.Id,
                UserId = order.UserId,
                ShippingTypeId = order.ShippingTypeId
            });
        }
    }
}
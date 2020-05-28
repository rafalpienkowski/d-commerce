using System;
using System.Threading.Tasks;
using Framework;
using Microsoft.AspNetCore.Components;
using Sales.Messages.Events;

namespace Web.Pages.PlaceOrder
{
    public class PlaceOrderBase : ComponentBase
    {
        protected PlaceOrderVm Order { get; } = new PlaceOrderVm();
        
        [Inject]
        public ICommandBus CommandBus { get; set; }
        
        [Inject]
        public IEventBus EventBus { get; set; }
        
        [Inject]
        public NavigationManager NavigationManager { get; set; }

        protected async Task HandleValidSubmit()
        {
            var placeOrderCommand = new Sales.Messages.Commands.PlaceOrder
            {
                Id = Guid.NewGuid(),
                UserId = "raf",
                ProductIds = Order.ProductIds.Split(","),
                ShippingTypeId = Order.ShippingTypeId,
                TimeStamp = DateTime.UtcNow
            };
            await CommandBus.Send(placeOrderCommand);
            await EventBus.Publish(new OrderPlaced
            {
                OrderId = placeOrderCommand.Id,
                UserId = placeOrderCommand.UserId,
                TimeStamp = DateTime.UtcNow
            });
            NavigationManager.NavigateTo($"placeorderconfirmation/{placeOrderCommand.Id}");
        }
    }
}
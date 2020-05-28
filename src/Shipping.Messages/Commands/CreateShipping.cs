using System;

namespace Shipping.Messages.Commands
{
    public class CreateShipping
    {
        public Guid OrderId { get; set; }
        public string UserId { get; set; }
        public string ShippingTypeId { get; set; }
    }
}
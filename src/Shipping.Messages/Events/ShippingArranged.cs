using System;

namespace Shipping.Messages.Events
{
    public class ShippingArranged
    {
        public Guid OrderId { get; set; }
    }
}
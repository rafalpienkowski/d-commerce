using System;
using Shipping.Framework;

namespace Shipping.BusinessCustomers
{
    public class ShippingArranged : DomainEvent
    {
        public Guid OrderId { get; set; }
    }
}
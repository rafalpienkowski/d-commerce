using System;
using MassTransit;

namespace Shipping.Messages.Events
{
    public class ShippingArranged : CorrelatedBy<Guid>
    {
        public Guid OrderId { get; set; }

        public Guid CorrelationId => OrderId;
    }
}
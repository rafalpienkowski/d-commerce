using System;
using MassTransit;

namespace Shipping.Messages.Commands
{
    public class CreateShipping : CorrelatedBy<Guid>
    {
        public Guid OrderId { get; set; }
        public string UserId { get; set; }
        public string ShippingTypeId { get; set; }

        public Guid CorrelationId => OrderId;
    }
}
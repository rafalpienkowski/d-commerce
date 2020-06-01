using System;
using MassTransit;

namespace Sales.Messages.Events
{
    public class OrderPlaced: CorrelatedBy<Guid>
    {
        public Guid OrderId { get; set; }
        public string UserId { get; set; }
        public string[] ProductIds { get; set; }
        public string ShippingTypeId { get; set; }
        public DateTime TimeStamp { get; set; }

        public Guid CorrelationId => OrderId;
    }
}
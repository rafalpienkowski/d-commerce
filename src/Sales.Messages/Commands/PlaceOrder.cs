using System;
using MassTransit;

namespace Sales.Messages.Commands
{
    public class PlaceOrder : CorrelatedBy<Guid>
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public string[] ProductIds { get; set; }
        public string ShippingTypeId { get; set; }
        public DateTime TimeStamp { get; set; }

        public Guid CorrelationId => Id;
    }
}
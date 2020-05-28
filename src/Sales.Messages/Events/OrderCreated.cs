using System;

namespace Sales.Messages.Events
{
    public class OrderCreated
    {
        public Guid OrderId { get; set; }
        public string Number { get; set; }
        public string UserId { get; set; }
        public string[] ProductIds { get; set; }
        public string ShippingTypeId { get; set; }
        public DateTime TimeStamp { get; set; }
        public decimal Amount { get; set; }
    }
}
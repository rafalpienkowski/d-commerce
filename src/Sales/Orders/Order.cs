using System;

namespace Sales.Orders
{
    public class Order
    {
        public Guid Id { get; set; }
        public string Number { get; set; }
        public string UserId { get; set; }
        public string ProductIds { get; set; }
        public string ShippingTypeId { get; set; }
        public DateTime TimeStamp { get; set; }
        public decimal Amount { get; set; }
    }
}
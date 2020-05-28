using System;

namespace Sales.Messages.Commands
{
    public class PlaceOrder
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public string[] ProductIds { get; set; }
        public string ShippingTypeId { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}
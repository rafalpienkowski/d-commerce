using System;

namespace Sales.Orders
{
    public class OrderPlaced
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public string Number { get; set; }
    }
}
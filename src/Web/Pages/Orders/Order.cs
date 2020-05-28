using System;

namespace Web.Pages.Orders
{
    public class Order
    {
        public Guid Id { get; set; }
        public string Number { get; set; }
        public string UserId { get; set; }
        public DateTime LastUpdate { get; set; }
        public string Status { get; set; }
    }
}
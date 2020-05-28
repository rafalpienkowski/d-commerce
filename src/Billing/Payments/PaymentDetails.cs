using System;
using System.ComponentModel.DataAnnotations;

namespace Billing.Payments
{
    public class PaymentDetails
    {
        public PaymentStatus Status { get; set; }
        public string CardNumber { get; set; }
        public decimal Amount { get; set; }
        public DateTime TimeStamp { get; set; }
        [Key]
        public Guid OrderId { get; set; }
    }

    public enum PaymentStatus
    {
        Pending = 0,
        Accepted = 1,
        Rejected = 2
    }
}
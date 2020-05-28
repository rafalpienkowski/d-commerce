using System;

namespace Billing.Payments
{
    public class PaymentConfirmation
    {
        public bool Succeed { get; set; }
        public string CardNumber { get; set; }
        public decimal Amount { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}
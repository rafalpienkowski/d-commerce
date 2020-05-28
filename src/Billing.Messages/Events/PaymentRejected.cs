using System;

namespace Billing.Messages.Events
{
    public class PaymentRejected
    {
        public Guid OrderId { get; set; }
    }
}
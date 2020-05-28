using System;

namespace Billing.Messages.Events
{
    public class PaymentConfirmed
    {
        public Guid OrderId { get; set; }
    }
}
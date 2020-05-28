using System;

namespace Billing.Messages.Events
{
    public class PaymentAccepted
    {
        public Guid OrderId { get; set; }
    }
}
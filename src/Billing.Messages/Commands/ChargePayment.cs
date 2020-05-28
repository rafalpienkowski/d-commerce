using System;

namespace Billing.Messages.Commands
{
    public class ChargePayment
    {
        public Guid OrderId { get; set; }
    }
}
using System;
using MassTransit;

namespace Billing.Messages.Events
{
    public class PaymentConfirmed : CorrelatedBy<Guid>
    {
        public Guid OrderId { get; set; }

        public Guid CorrelationId => OrderId;
    }
}
using System;
using MassTransit;

namespace Billing.Messages.Commands
{
    public class ChargePayment : CorrelatedBy<Guid>
    {
        public Guid OrderId { get; set; }

        public Guid CorrelationId => OrderId;
    }
}
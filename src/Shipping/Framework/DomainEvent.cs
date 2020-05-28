using System;

namespace Shipping.Framework
{
    public abstract class DomainEvent
    {
        public Guid Id { get; } = Guid.NewGuid();
        public DateTime TimeStamp { get; } = DateTime.UtcNow;
    }
}
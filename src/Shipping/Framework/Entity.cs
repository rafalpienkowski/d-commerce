using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Shipping.Framework
{
    public abstract class Entity
    {
        protected IList<DomainEvent> PendingEvents { get; } = new List<DomainEvent>();
        public IReadOnlyCollection<DomainEvent> GetPendingEvents() => new ReadOnlyCollection<DomainEvent>(PendingEvents);
        
    }
}
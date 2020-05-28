using System;

namespace Shipping.Framework
{
    public class PersistedEvent
    {
        public Guid Id { get; set; }
        public DateTime TimeStamp { get; set; }
        public string Type { get; set; }
        public string Body { get; set; }
        public bool Processed { get; set; }
    }
}
using System;
using System.ComponentModel.DataAnnotations;

namespace Shipping.Framework
{
    public class EventContext
    {
        [Key]
        public Guid EventId { get; set; }
        public string TraceId { get; set; }
        public string SpanId { get; set; }
    }
}
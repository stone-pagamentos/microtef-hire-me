using System;
using System.Collections.Generic;

namespace EFCoreMapStoneV13
{
    public partial class StoredEvent
    {
        public Guid Id { get; set; }
        public Guid AggregateId { get; set; }
        public string Data { get; set; }
        public string Action { get; set; }
        public DateTime CreationDate { get; set; }
        public string User { get; set; }
    }
}
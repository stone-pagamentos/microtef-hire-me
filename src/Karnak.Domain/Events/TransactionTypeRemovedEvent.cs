using System;
using Karnak.Domain.Core.Events;

namespace Karnak.Domain.Events
{
    public class TransactionTypeRemovedEvent : Event
    {
        public TransactionTypeRemovedEvent(Guid id)
        {
            Id = id;
            AggregateId = id;
        }

        public Guid Id { get; set; }
    }
}
using System;
using Karnak.Domain.Core.Events;

namespace Karnak.Domain.Events
{
    public class CardTypeRemovedEvent : Event
    {
        public CardTypeRemovedEvent(Guid id)
        {
            Id = id;
            AggregateId = id;
        }

        public Guid Id { get; set; }
    }
}
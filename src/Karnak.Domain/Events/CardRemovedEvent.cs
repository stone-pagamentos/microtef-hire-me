using System;
using Karnak.Domain.Core.Events;

namespace Karnak.Domain.Events
{
    public class CardRemovedEvent : Event
    {
        public CardRemovedEvent(Guid id)
        {
            Id = id;
            AggregateId = id;
        }

        public Guid Id { get; set; }
    }
}
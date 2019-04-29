using System;
using Karnak.Domain.Core.Events;

namespace Karnak.Domain.Events
{
    public class CardBrandRemovedEvent : Event
    {
        public CardBrandRemovedEvent(Guid id)
        {
            Id = id;
            AggregateId = id;
        }

        public Guid Id { get; set; }
    }
}
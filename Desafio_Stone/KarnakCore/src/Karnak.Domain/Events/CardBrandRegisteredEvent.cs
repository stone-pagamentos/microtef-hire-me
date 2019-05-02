using System;
using Karnak.Domain.Core.Events;

namespace Karnak.Domain.Events
{
    public class CardBrandRegisteredEvent : Event
    {
        public CardBrandRegisteredEvent(
            Guid id,
            string _Name
        )
        {
            Id = id;
            Name = _Name;
            AggregateId = id;
        }
        public Guid Id { get; set; }

        public string Name { get; private set; }
    }
}
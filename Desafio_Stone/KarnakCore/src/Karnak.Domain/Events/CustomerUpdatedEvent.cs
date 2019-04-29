using System;
using Karnak.Domain.Core.Events;

namespace Karnak.Domain.Events
{
    public class CustomerUpdatedEvent : Event
    {
        public CustomerUpdatedEvent(
            Guid id,
            string _Name,
            string _Email,
            DateTime _BirthDate
        )
        {
            Id = id;
            Name = _Name;
            BirthDate = _BirthDate;
            Email = _Email;
            AggregateId = id;
        }

        public Guid Id { get; set; }

        public string Name { get; private set; }

        public string Email { get; private set; }

        public DateTime BirthDate { get; private set; }
    }
}
using System;
using System.Collections.Generic;
using Karnak.Domain.Core.Models;

namespace Karnak.Domain.Models
{
    public class Customer : Entity
    {
        public Customer(
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
        }

        public Customer()
        {
            Card = new HashSet<Card>();
        }

        public string Name { get; private set; }

        public DateTime BirthDate { get; private set; }

        public string Email { get; private set; }

        public virtual ICollection<Card> Card { get; private set; }
    }
}

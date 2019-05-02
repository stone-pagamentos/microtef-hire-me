using Karnak.Domain.Core.Models;
using System;
using System.Collections.Generic;

namespace Karnak.Domain.Models
{
    public class CardType : Entity
    {
        public CardType(
            Guid id,
            string _Name
        )
        {
            Id = id;
            Name = _Name;
        }

        public CardType()
        {
            Card = new HashSet<Card>();
        }

        public string Name { get; private set; }

        public virtual ICollection<Card> Card { get; private set; }
    }
}

using Karnak.Domain.Core.Models;
using System;
using System.Collections.Generic;

namespace Karnak.Domain.Models
{
    public class CardBrand : Entity
    {
        public CardBrand(
            Guid id,
            string _Name
        )
        {
            Id = id;
            Name = _Name;
        }

        protected CardBrand()
        {
            Card = new HashSet<Card>();
        }

        public string Name { get; private set; }

        public virtual ICollection<Card> Card { get; private set; }
    }
}

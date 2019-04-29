using System;
using System.Collections.Generic;

namespace UnitTesteKarnakStone.Models
{
    public partial class CardBrand
    {
        public CardBrand()
        {
            Card = new HashSet<Card>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Card> Card { get; set; }
    }
}
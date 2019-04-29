﻿using System;
using System.Collections.Generic;

namespace EFCoreMapStoneV13
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
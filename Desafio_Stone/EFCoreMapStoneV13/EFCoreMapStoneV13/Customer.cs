using System;
using System.Collections.Generic;

namespace EFCoreMapStoneV13
{
    public partial class Customer
    {
        public Customer()
        {
            Card = new HashSet<Card>();
        }

        public Guid Id { get; set; }
        public DateTime BirthDate { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Card> Card { get; set; }
    }
}
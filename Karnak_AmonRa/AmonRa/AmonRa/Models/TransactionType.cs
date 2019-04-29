using System;
using System.Collections.Generic;

namespace AmonRa.Models
{
    public partial class TransactionType
    {
        //public TransactionType()
        //{
        //    Transaction = new HashSet<Transaction>();
        //}

        public Guid Id { get; set; }
        public string Name { get; set; }

        //public virtual ICollection<Transaction> Transaction { get; set; }
    }
}
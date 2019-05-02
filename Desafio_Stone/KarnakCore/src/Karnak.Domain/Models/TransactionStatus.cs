using Karnak.Domain.Core.Models;
using System;
using System.Collections.Generic;

namespace Karnak.Domain.Models
{
    public class TransactionStatus : Entity
    {
        public TransactionStatus(
            Guid id,
            string _Name
        )
        {
            Id = id;
            Name = _Name;
        }

        public TransactionStatus()
        {
            Transaction = new HashSet<Transaction>();
        }

        public string Name { get; private set; }

        public virtual ICollection<Transaction> Transaction { get; private set; }
    }
}

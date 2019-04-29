using System;
using System.Collections.Generic;

namespace EFCoreMapStoneV13
{
    public partial class TransactionStatus
    {
        public TransactionStatus()
        {
            Transaction = new HashSet<Transaction>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Transaction> Transaction { get; set; }
    }
}
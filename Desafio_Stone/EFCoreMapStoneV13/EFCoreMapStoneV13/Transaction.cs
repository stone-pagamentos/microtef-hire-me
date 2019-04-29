using System;
using System.Collections.Generic;

namespace EFCoreMapStoneV13
{
    public partial class Transaction
    {
        public Guid Id { get; set; }
        public decimal Amount { get; set; }
        public Guid IdTransactionType { get; set; }
        public Guid IdCard { get; set; }
        public Guid IdTransactionStatus { get; set; }
        public int Number { get; set; }
        public DateTime TransactionDate { get; set; }

        public virtual Card IdCardNavigation { get; set; }
        public virtual TransactionStatus IdTransactionStatusNavigation { get; set; }
        public virtual TransactionType IdTransactionTypeNavigation { get; set; }
    }
}
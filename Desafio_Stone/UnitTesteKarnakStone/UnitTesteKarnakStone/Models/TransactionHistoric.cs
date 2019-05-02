using System;
using System.Collections.Generic;

namespace UnitTesteKarnakStone.Models
{
    public partial class TransactionHistoric
    {
        public Guid Id { get; set; }
        public decimal Amount { get; set; }
        public Guid IdTransactionType { get; set; }
        public Guid IdCard { get; set; }
        public Guid IdTransactionStatus { get; set; }
        public int Number { get; set; }
        public DateTime TransactionDate { get; set; }
        public string Action { get; set; }
        public string When { get; set; }
        public string Who { get; set; }

        public virtual Card IdCardNavigation { get; set; }
        public virtual TransactionStatus IdTransactionStatusNavigation { get; set; }
        public virtual TransactionType IdTransactionTypeNavigation { get; set; }
    }
}
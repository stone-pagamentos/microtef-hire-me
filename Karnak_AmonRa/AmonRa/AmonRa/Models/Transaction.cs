using System;
using System.Collections.Generic;

namespace AmonRa.Models
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
        public string Password { get; set; }
        public string HasPassword { get; set; }
    }
}
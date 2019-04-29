using Karnak.Domain.Core.Models;
using System;

namespace Karnak.Domain.Models
{
    public class Transaction : Entity
    {
        public Transaction(
            Guid id,
            decimal _Amount,
            Guid _IdTransactionType,
            Guid _IdCard,
            Guid _IdTransactionStatus,
            int _Number,
            DateTime _TransactionDate
        )
        {
            Id = id;
            Amount = _Amount;
            IdTransactionType = _IdTransactionType;
            IdCard = _IdCard;
            IdTransactionStatus = _IdTransactionStatus;
            Number = _Number;
            TransactionDate = _TransactionDate;
        }

        // Empty constructor for EF
        protected Transaction() { }

        public decimal Amount { get; private set; }
        public Guid IdTransactionType { get; private set; }
        public Guid IdCard { get; private set; }
        public Guid IdTransactionStatus { get; private set; }
        public int Number { get; private set; }
        public DateTime TransactionDate { get; private set; }

        public virtual Card IdCardNavigation { get; private set; }
        public virtual TransactionStatus IdTransactionStatusNavigation { get; private set; }
        public virtual TransactionType IdTransactionTypeNavigation { get; private set; }
    }
}

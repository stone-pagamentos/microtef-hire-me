using System;
using Karnak.Domain.Core.Events;

namespace Karnak.Domain.Events
{
    public class TransactionRegisteredEvent : Event
    {
        public TransactionRegisteredEvent(
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
            AggregateId = id;
        }

        public Guid Id { get; set; }
        public decimal Amount { get; private set; }
        public Guid IdTransactionType { get; private set; }
        public Guid IdCard { get; private set; }
        public Guid IdTransactionStatus { get; private set; }
        public int Number { get; private set; }
        public DateTime TransactionDate { get; private set; }
    }
}
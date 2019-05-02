using System;
using Karnak.Domain.Core.Events;

namespace Karnak.Domain.Events
{
    public class CardRegisteredEvent : Event
    {
        public CardRegisteredEvent(
            Guid id,
            Guid _IdCustomer,
            Guid _IdBrand,
            Guid _IdCardType,
            string _CardNumber,
            DateTime _ExpirationDate,
            int _HasPassword,
            string _Password,
            decimal _Limit,
            decimal _LimitAvailable,
            int _Attempts,
            int _Blocked
        )
        {
            Id = id;
            IdCardType = _IdCardType;
            IdCustomer = _IdCustomer;
            IdBrand = _IdBrand;
            CardNumber = _CardNumber;
            ExpirationDate = _ExpirationDate;
            HasPassword = _HasPassword;
            Password = _Password;
            Limit = _Limit;
            LimitAvailable = _LimitAvailable;
            Attempts = _Attempts;
            Blocked = _Blocked;
            AggregateId = id;
        }

        public Guid Id { get; set; }
        public Guid IdCardType { get; private set; }
        public Guid IdCustomer { get; private set; }
        public Guid IdBrand { get; private set; }
        public string CardNumber { get; private set; }
        public DateTime ExpirationDate { get; private set; }
        public int HasPassword { get; private set; }
        public string Password { get; private set; }
        public decimal Limit { get; private set; }
        public decimal LimitAvailable { get; private set; }
        public int Attempts { get; private set; }
        public int Blocked { get; private set; }
    }
}
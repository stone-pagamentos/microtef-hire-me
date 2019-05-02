using Karnak.Domain.Core.Models;
using System;
using System.Collections.Generic;

namespace Karnak.Domain.Models
{
    public class Card : Entity
    {
        public Card(
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
        }

        protected Card()
        {
            Transaction = new HashSet<Transaction>();
        }

        public Guid IdCardType { get; private set; }
        public Guid IdCustomer { get; private set; }
        public Guid IdBrand { get; private set; }
        public string CardNumber { get; private set; }
        public DateTime ExpirationDate { get; private set; }
        public int HasPassword { get; private set; }
        public string Password { get; private set; }
        public decimal Limit { get; private set; }
        public decimal LimitAvailable { get; set; }
        public int Attempts { get; set; }
        public int Blocked { get; set; }

        public virtual CardBrand IdBrandNavigation { get; private set; }
        public virtual CardType IdCardTypeNavigation { get; private set; }
        public virtual Customer IdCustomerNavigation { get; private set; }
        public virtual ICollection<Transaction> Transaction { get; set; }
    }
}

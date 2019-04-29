using Karnak.Domain.Core.Commands;
using System;

namespace Karnak.Domain.Commands
{
    public abstract class CardCommand : Command
    {
        public Guid Id { get; protected set; }
        public Guid IdCardType { get; protected set; }
        public Guid IdCustomer { get; protected set; }
        public Guid IdBrand { get; protected set; }
        public string CardNumber { get; protected set; }
        public DateTime ExpirationDate { get; protected set; }
        public int HasPassword { get; protected set; }
        public string Password { get; protected set; }
        public decimal Limit { get; protected set; }
        public decimal LimitAvailable { get; protected set; }
        public int Attempts { get; protected set; }
        public int Blocked { get; protected set; }
    }
}

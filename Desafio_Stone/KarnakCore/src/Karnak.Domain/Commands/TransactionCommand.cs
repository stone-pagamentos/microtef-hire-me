using Karnak.Domain.Core.Commands;
using System;

namespace Karnak.Domain.Commands
{
    public abstract class TransactionCommand : Command
    {
        public Guid Id { get; protected set; } 
        public decimal Amount { get; protected set; }
        public Guid IdTransactionType { get; protected set; }
        public Guid IdCard { get; protected set; }
        public Guid IdTransactionStatus { get; protected set; }
        public int Number { get; protected set; }
        public DateTime TransactionDate { get; protected set; }
        public string Password { get; protected set; }
        public string HasPassword { get; protected set; }
    }
}

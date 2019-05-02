using Karnak.Domain.Validations;
using System;

namespace Karnak.Domain.Commands
{
    public class RegisterNewTransactionCommand : TransactionCommand
    {
        public RegisterNewTransactionCommand(
            decimal _Amount,
            Guid _IdTransactionType,
            Guid _IdCard,
            Guid _IdTransactionStatus,
            int _Number,
            DateTime _TransactionDate,
            string _Password,
            string _HasPassword
        )
        {
            Amount = _Amount;
            IdTransactionType = _IdTransactionType;
            IdCard = _IdCard;
            IdTransactionStatus = _IdTransactionStatus;
            Number = _Number;
            TransactionDate = _TransactionDate;
            Password = _Password;
            HasPassword = _HasPassword;
        }

        public override bool IsValid()
        {
            ValidationResult = new RegisterNewTransactionCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
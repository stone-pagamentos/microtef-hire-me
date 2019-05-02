using System;
using Karnak.Domain.Validations;

namespace Karnak.Domain.Commands
{
    public class UpdateTransactionCommand : TransactionCommand
    {
        public UpdateTransactionCommand(
            Guid id,
            decimal _Amount,
            Guid _IdTransactionType,
            Guid _IdCard,
            Guid _IdTransactionStatus,
            int _Number,
            DateTime _TransactionDate,
            String _Password
        )
        {
            Id = id;
            Amount = _Amount;
            IdTransactionType = _IdTransactionType;
            IdCard = _IdCard;
            IdTransactionStatus = _IdTransactionStatus;
            Number = _Number;
            TransactionDate = _TransactionDate;
            Password = _Password;
        }

        public override bool IsValid()
        {
            ValidationResult = new UpdateTransactionCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
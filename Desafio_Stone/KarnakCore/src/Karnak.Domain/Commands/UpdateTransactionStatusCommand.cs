using System;
using Karnak.Domain.Validations;

namespace Karnak.Domain.Commands
{
    public class UpdateTransactionStatusCommand : TransactionStatusCommand
    {
        public UpdateTransactionStatusCommand(
            Guid id, 
            string _Name
        )
        {
            Id = id;
            Name = _Name;
        }

        public override bool IsValid()
        {
            ValidationResult = new UpdateTransactionStatusCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
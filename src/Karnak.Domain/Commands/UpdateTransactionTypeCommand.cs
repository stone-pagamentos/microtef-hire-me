using System;
using Karnak.Domain.Validations;

namespace Karnak.Domain.Commands
{
    public class UpdateTransactionTypeCommand : TransactionTypeCommand
    {
        public UpdateTransactionTypeCommand(
            Guid id, 
            string _Name
        )
        {
            Id = id;
            Name = _Name;
        }

        public override bool IsValid()
        {
            ValidationResult = new UpdateTransactionTypeCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
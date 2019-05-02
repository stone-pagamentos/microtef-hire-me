using System;
using Karnak.Domain.Validations;

namespace Karnak.Domain.Commands
{
    public class RemoveTransactionTypeCommand : TransactionTypeCommand
    {
        public RemoveTransactionTypeCommand(
            Guid id
        )
        {
            Id = id;
            AggregateId = id;
        }

        public override bool IsValid()
        {
            ValidationResult = new RemoveTransactionTypeCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
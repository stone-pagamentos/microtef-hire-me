using System;
using Karnak.Domain.Validations;

namespace Karnak.Domain.Commands
{
    public class RemoveTransactionCommand : TransactionCommand
    {
        public RemoveTransactionCommand(
            Guid id
        )
        {
            Id = id;
            AggregateId = id;
        }

        public override bool IsValid()
        {
            ValidationResult = new RemoveTransactionCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
using System;
using Karnak.Domain.Validations;

namespace Karnak.Domain.Commands
{
    public class RemoveTransactionStatusCommand : TransactionStatusCommand
    {
        public RemoveTransactionStatusCommand(
            Guid id
        )
        {
            Id = id;
            AggregateId = id;
        }

        public override bool IsValid()
        {
            ValidationResult = new RemoveTransactionStatusCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
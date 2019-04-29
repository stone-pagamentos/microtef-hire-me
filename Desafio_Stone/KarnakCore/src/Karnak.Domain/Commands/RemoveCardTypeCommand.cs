using System;
using Karnak.Domain.Validations;

namespace Karnak.Domain.Commands
{
    public class RemoveCardTypeCommand : CardTypeCommand
    {
        public RemoveCardTypeCommand(
            Guid id
        )
        {
            Id = id;
            AggregateId = id;
        }

        public override bool IsValid()
        {
            ValidationResult = new RemoveCardTypeCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
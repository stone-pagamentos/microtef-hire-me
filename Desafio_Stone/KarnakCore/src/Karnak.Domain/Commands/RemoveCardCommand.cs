using System;
using Karnak.Domain.Validations;

namespace Karnak.Domain.Commands
{
    public class RemoveCardCommand : CardCommand
    {
        public RemoveCardCommand(
            Guid id
        )
        {
            Id = id;
            AggregateId = id;
        }

        public override bool IsValid()
        {
            ValidationResult = new RemoveCardCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
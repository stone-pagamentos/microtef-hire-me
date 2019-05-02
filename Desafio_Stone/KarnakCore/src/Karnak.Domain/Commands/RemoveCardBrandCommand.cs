using System;
using Karnak.Domain.Validations;

namespace Karnak.Domain.Commands
{
    public class RemoveCardBrandCommand : CardBrandCommand
    {
        public RemoveCardBrandCommand(
            Guid id
        )
        {
            Id = id;
            AggregateId = id;
        }

        public override bool IsValid()
        {
            ValidationResult = new RemoveCardBrandCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
using System;
using Karnak.Domain.Validations;

namespace Karnak.Domain.Commands
{
    public class UpdateCardBrandCommand : CardBrandCommand
    {
        public UpdateCardBrandCommand(
            Guid id, 
            string _Name
        )
        {
            Id = id;
            Name = _Name;
        }

        public override bool IsValid()
        {
            ValidationResult = new UpdateCardBrandCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
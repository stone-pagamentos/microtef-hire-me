using System;
using Karnak.Domain.Validations;

namespace Karnak.Domain.Commands
{
    public class UpdateCardTypeCommand : CardTypeCommand
    {
        public UpdateCardTypeCommand(
            Guid id, 
            string _Name
        )
        {
            Id = id;
            Name = _Name;
        }

        public override bool IsValid()
        {
            ValidationResult = new UpdateCardTypeCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
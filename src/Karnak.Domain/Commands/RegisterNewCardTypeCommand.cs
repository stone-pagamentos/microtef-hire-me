using Karnak.Domain.Validations;

namespace Karnak.Domain.Commands
{
    public class RegisterNewCardTypeCommand : CardTypeCommand
    {
        public RegisterNewCardTypeCommand(
            string _Name
        )
        {
            Name = _Name;
        }

        public override bool IsValid()
        {
            ValidationResult = new RegisterNewCardTypeCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
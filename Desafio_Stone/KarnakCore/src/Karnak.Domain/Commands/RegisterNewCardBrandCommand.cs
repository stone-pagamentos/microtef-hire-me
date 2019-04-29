using Karnak.Domain.Validations;

namespace Karnak.Domain.Commands
{
    public class RegisterNewCardBrandCommand : CardBrandCommand
    {
        public RegisterNewCardBrandCommand(
            string _Name
        )
        {
            Name = _Name;
        }

        public override bool IsValid()
        {
            ValidationResult = new RegisterNewCardBrandCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
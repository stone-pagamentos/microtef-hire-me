using Karnak.Domain.Commands;

namespace Karnak.Domain.Validations
{
    public class RegisterNewCardTypeCommandValidation : CardTypeValidation<RegisterNewCardTypeCommand>
    {
        public RegisterNewCardTypeCommandValidation()
        {
            ValidateId();
            ValidateName();
        }
    }
}
using Karnak.Domain.Commands;

namespace Karnak.Domain.Validations
{
    public class RegisterNewCardBrandCommandValidation : CardBrandValidation<RegisterNewCardBrandCommand>
    {
        public RegisterNewCardBrandCommandValidation()
        {
            ValidateId();
            ValidateName();
        }
    }
}
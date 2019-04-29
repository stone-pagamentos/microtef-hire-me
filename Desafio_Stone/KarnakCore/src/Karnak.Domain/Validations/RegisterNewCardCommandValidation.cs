using Karnak.Domain.Commands;

namespace Karnak.Domain.Validations
{
    public class RegisterNewCardCommandValidation : CardValidation<RegisterNewCardCommand>
    {
        public RegisterNewCardCommandValidation()
        {
            ValidateId();
            ValidateCardNumber();
            ValidateExpirationDate();
            ValidatePasswordEmpty();
            ValidatePasswordLength();
            ValidateLimit();
            ValidateIdCardType();
            ValidateIdCustomer();
            ValidateIdBrand();
        }
    }
}
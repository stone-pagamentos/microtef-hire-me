using Karnak.Domain.Commands;

namespace Karnak.Domain.Validations
{
    public class UpdateCardCommandValidation : CardValidation<UpdateCardCommand>
    {
        public UpdateCardCommandValidation()
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
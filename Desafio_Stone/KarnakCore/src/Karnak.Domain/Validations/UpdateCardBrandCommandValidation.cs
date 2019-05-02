using Karnak.Domain.Commands;

namespace Karnak.Domain.Validations
{
    public class UpdateCardBrandCommandValidation : CardBrandValidation<UpdateCardBrandCommand>
    {
        public UpdateCardBrandCommandValidation()
        {
            ValidateId();
            ValidateName();
        }
    }
}
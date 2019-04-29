using Karnak.Domain.Commands;

namespace Karnak.Domain.Validations
{
    public class RemoveCardBrandCommandValidation : CardBrandValidation<RemoveCardBrandCommand>
    {
        public RemoveCardBrandCommandValidation()
        {
            ValidateId();
        }
    }
}
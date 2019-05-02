using Karnak.Domain.Commands;

namespace Karnak.Domain.Validations
{
    public class RemoveCardCommandValidation : CardValidation<RemoveCardCommand>
    {
        public RemoveCardCommandValidation()
        {
            ValidateId();
        }
    }
}
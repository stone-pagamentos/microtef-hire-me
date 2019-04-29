using Karnak.Domain.Commands;

namespace Karnak.Domain.Validations
{
    public class RemoveCardTypeCommandValidation : CardTypeValidation<RemoveCardTypeCommand>
    {
        public RemoveCardTypeCommandValidation()
        {
            ValidateId();
        }
    }
}
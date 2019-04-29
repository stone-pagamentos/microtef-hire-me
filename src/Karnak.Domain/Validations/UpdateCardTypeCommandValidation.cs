using Karnak.Domain.Commands;

namespace Karnak.Domain.Validations
{
    public class UpdateCardTypeCommandValidation : CardTypeValidation<UpdateCardTypeCommand>
    {
        public UpdateCardTypeCommandValidation()
        {
            ValidateId();
            ValidateName();
        }
    }
}
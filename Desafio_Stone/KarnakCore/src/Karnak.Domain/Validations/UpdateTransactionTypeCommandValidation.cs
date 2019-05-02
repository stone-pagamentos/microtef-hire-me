using Karnak.Domain.Commands;

namespace Karnak.Domain.Validations
{
    public class UpdateTransactionTypeCommandValidation : TransactionTypeValidation<UpdateTransactionTypeCommand>
    {
        public UpdateTransactionTypeCommandValidation()
        {
            ValidateId();
            ValidateName();
        }
    }
}
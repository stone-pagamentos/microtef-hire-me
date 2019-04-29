using Karnak.Domain.Commands;

namespace Karnak.Domain.Validations
{
    public class RemoveTransactionTypeCommandValidation : TransactionTypeValidation<RemoveTransactionTypeCommand>
    {
        public RemoveTransactionTypeCommandValidation()
        {
            ValidateId();
        }
    }
}
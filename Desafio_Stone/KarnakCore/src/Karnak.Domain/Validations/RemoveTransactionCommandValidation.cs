using Karnak.Domain.Commands;

namespace Karnak.Domain.Validations
{
    public class RemoveTransactionCommandValidation : TransactionValidation<RemoveTransactionCommand>
    {
        public RemoveTransactionCommandValidation()
        {
            ValidateId();
        }
    }
}
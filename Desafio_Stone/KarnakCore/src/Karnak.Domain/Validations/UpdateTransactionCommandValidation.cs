using Karnak.Domain.Commands;

namespace Karnak.Domain.Validations
{
    public class UpdateTransactionCommandValidation : TransactionValidation<UpdateTransactionCommand>
    {
        public UpdateTransactionCommandValidation()
        {
            ValidateId();
            ValidateAmountMinimum10Cents();
        }
    }
}
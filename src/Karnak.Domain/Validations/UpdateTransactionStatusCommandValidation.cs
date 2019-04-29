using Karnak.Domain.Commands;

namespace Karnak.Domain.Validations
{
    public class UpdateTransactionStatusCommandValidation : TransactionStatusValidation<UpdateTransactionStatusCommand>
    {
        public UpdateTransactionStatusCommandValidation()
        {
            ValidateId();
            ValidateName();
        }
    }
}
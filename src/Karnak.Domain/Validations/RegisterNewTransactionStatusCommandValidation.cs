using Karnak.Domain.Commands;

namespace Karnak.Domain.Validations
{
    public class RegisterNewTransactionStatusCommandValidation : TransactionStatusValidation<RegisterNewTransactionStatusCommand>
    {
        public RegisterNewTransactionStatusCommandValidation()
        {
            ValidateId();
            ValidateName();
        }
    }
}
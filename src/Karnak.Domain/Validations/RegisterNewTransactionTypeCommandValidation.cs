using Karnak.Domain.Commands;

namespace Karnak.Domain.Validations
{
    public class RegisterNewTransactionTypeCommandValidation : TransactionTypeValidation<RegisterNewTransactionTypeCommand>
    {
        public RegisterNewTransactionTypeCommandValidation()
        {
            ValidateId();
            ValidateName();
        }
    }
}
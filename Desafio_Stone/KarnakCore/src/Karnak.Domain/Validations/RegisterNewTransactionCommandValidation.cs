using Karnak.Domain.Commands;

namespace Karnak.Domain.Validations
{
    public class RegisterNewTransactionCommandValidation : TransactionValidation<RegisterNewTransactionCommand>
    {
        public RegisterNewTransactionCommandValidation()
        {
            ValidateId();
            PasswordBetween4and6Digits();
            ValidateTransactionTypeId();
            ValidateTransactionCardId();
            ValidateAmountMinimum10Cents();
        }
    }
}
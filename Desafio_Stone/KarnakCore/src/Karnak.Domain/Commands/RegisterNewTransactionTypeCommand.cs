using Karnak.Domain.Validations;

namespace Karnak.Domain.Commands
{
    public class RegisterNewTransactionTypeCommand : TransactionTypeCommand
    {
        public RegisterNewTransactionTypeCommand(
            string _Name    
        )
        {
            Name = _Name;
        }

        public override bool IsValid()
        {
            ValidationResult = new RegisterNewTransactionTypeCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
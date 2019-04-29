using Karnak.Domain.Validations;

namespace Karnak.Domain.Commands
{
    public class RegisterNewTransactionStatusCommand : TransactionStatusCommand
    {
        public RegisterNewTransactionStatusCommand(
            string _Name    
        )
        {
            Name = _Name;
        }

        public override bool IsValid()
        {
            ValidationResult = new RegisterNewTransactionStatusCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
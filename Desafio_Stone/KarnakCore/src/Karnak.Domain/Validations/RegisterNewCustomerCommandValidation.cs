using Karnak.Domain.Commands;

namespace Karnak.Domain.Validations
{
    public class RegisterNewCustomerCommandValidation : CustomerValidation<RegisterNewCustomerCommand>
    {
        public RegisterNewCustomerCommandValidation()
        {
            ValidateId();
            ValidateName();
            ValidateEmail();
        }
    }
}
using Karnak.Domain.Commands;

namespace Karnak.Domain.Validations
{
    public class UpdateCustomerCommandValidation : CustomerValidation<UpdateCustomerCommand>
    {
        public UpdateCustomerCommandValidation()
        {
            ValidateId();
            ValidateName();
            ValidateEmail();
        }
    }
}
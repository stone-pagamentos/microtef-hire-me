using Karnak.Domain.Validations;
using System;

namespace Karnak.Domain.Commands
{
    public class RegisterNewCardCommand : CardCommand
    {
        public RegisterNewCardCommand(
            Guid _IdCustomer,
            Guid _IdBrand,
            Guid _IdCardType,
            string _CardNumber,
            DateTime _ExpirationDate,
            int _HasPassword,
            string _Password,
            decimal _Limit,
            decimal _LimitAvailable,
            int _Attempts,
            int _Blocked
        )
        {
            IdCardType = _IdCardType;
            IdCustomer = _IdCustomer;
            IdBrand = _IdBrand;
            CardNumber = _CardNumber;
            ExpirationDate = _ExpirationDate;
            HasPassword = _HasPassword;
            Password = _Password;
            Limit = _Limit;
            LimitAvailable = _LimitAvailable;
            Attempts = _Attempts;
            Blocked = _Blocked;
        }

        public override bool IsValid()
        {
            ValidationResult = new RegisterNewCardCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
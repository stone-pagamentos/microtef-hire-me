using System;
using Karnak.Domain.Validations;

namespace Karnak.Domain.Commands
{
    public class UpdateCardCommand : CardCommand
    {
        public UpdateCardCommand(
            Guid id,
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
            Id = id;
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
            ValidationResult = new UpdateCardCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
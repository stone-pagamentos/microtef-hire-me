using System;
using Karnak.Domain.Commands;
using FluentValidation;
using Karnak.Domain.Common;

namespace Karnak.Domain.Validations
{
    public abstract class CardValidation<T> : AbstractValidator<T> where T : CardCommand
    {
        protected void ValidateId()
        {
            RuleFor(c => c.Id)
                .NotEqual(Guid.Empty).WithMessage("The Guid is empty")
                .Custom((info, context) =>
                {
                    if (!GuidValidation.GuidTryParse(info))
                    {
                        context.AddFailure("The Guid is invalid");
                    }
                })
                .Custom((info, context) =>
                {
                    if (Convert.ToString(info).Contains("00000000"))
                    {
                        context.AddFailure("The Guid is invalid and contains 00000000");
                    }
                });
        }

        protected void ValidateIdCardType()
        {
            RuleFor(c => c.IdCardType)
                .Custom((info, context) =>
                {
                    if (!GuidValidation.GuidTryParse(info))
                    {
                        context.AddFailure("The guid is invalid to card type");
                    }
                });
        }

        protected void ValidateIdCustomer()
        {
            RuleFor(c => c.IdCustomer)
                .Custom((info, context) =>
                {
                    if (!GuidValidation.GuidTryParse(info))
                    {
                        context.AddFailure("The guid is invalid to customer");
                    }
                });
        }

        protected void ValidateIdBrand()
        {
            RuleFor(c => c.IdBrand)
                .Custom((info, context) =>
                {
                    if (!GuidValidation.GuidTryParse(info))
                    {
                        context.AddFailure("The guid is invalid to brand");
                    }
                });
        }

        protected void ValidateCardNumber()
        {
            RuleFor(c => c.CardNumber)
                .Length(12, 19).WithMessage("The card number must have between 12 and 19 digits");
        }

        protected void ValidateExpirationDate()
        {
            RuleFor(c => c.ExpirationDate)
                .Custom((info, context) =>
                {
                    if (DateTime.Compare(info, System.DateTime.Now) == -1)
                    {
                        context.AddFailure("The expiration date must have greater than today");
                    }
                });
        }

        protected void ValidatePasswordLength()
        {
            RuleFor(c => c)
                .Custom((info, context) =>
                {
                    if (info.HasPassword == 1 
                        && StringCipher.Decrypt(info.Password, "StefanSilva@#@Stone##2019").Length < 4 
                        || StringCipher.Decrypt(info.Password, "StefanSilva@#@Stone##2019").Length > 6
                    )
                    {
                        context.AddFailure("The password length must have between 4 and 6 digits");
                    }
                });
        }

        protected void ValidatePasswordEmpty()
        {
            RuleFor(c => c)
                .Custom((info, context) =>
                {
                    if (info.HasPassword == 1 
                        && StringCipher.Decrypt(info.Password, "StefanSilva@#@Stone##2019").Length == 0
                    )
                    {
                        context.AddFailure("The password is invalid");
                    }
                });
        }

        protected void ValidateLimit()
        {
            RuleFor(c => c.Limit)
                .Custom((info, context) =>
                {
                    if (info <= 0)
                    {
                        context.AddFailure("The limit must have greater than zero");
                    }
                });
        }
    }
}
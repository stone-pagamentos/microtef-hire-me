using System;
using Karnak.Domain.Commands;
using FluentValidation;
using Karnak.Domain.Common;

namespace Karnak.Domain.Validations
{
    public abstract class TransactionValidation<T> : AbstractValidator<T> where T : TransactionCommand
    {
        protected void ValidateId()
        {
            RuleFor(c => c.Id)
                .NotEqual(Guid.Empty).WithMessage("The transacion guid is empty")
                .Custom((info, context) =>
                {
                    if (!GuidValidation.GuidTryParse(info))
                    {
                        context.AddFailure("The transaction guid is invalid");
                    }
                })
                .Custom((info, context) =>
                {
                    if (Convert.ToString(info).Contains("00000000"))
                    {
                        context.AddFailure("The transaction guid is invalid and contains 00000000");
                    }
                });
        }

        protected void ValidateTransactionTypeId()
        {
            RuleFor(c => c.IdTransactionType)
                .NotEqual(Guid.Empty).WithMessage("The transacion type guid is empty")
                .Custom((info, context) =>
                {
                    if (!GuidValidation.GuidTryParse(info))
                    {
                        context.AddFailure("The transaction type guid is invalid");
                    }
                })
                .Custom((info, context) =>
                {
                    if (Convert.ToString(info).Contains("00000000"))
                    {
                        context.AddFailure("The transaction type guid is invalid and contains 00000000");
                    }
                });
        }

        protected void ValidateTransactionCardId()
        {
            RuleFor(c => c.IdCard)
                .NotEqual(Guid.Empty).WithMessage("The transacion card guid is empty")
                .Custom((info, context) =>
                {
                    if (!GuidValidation.GuidTryParse(info))
                    {
                        context.AddFailure("The transaction card guid is invalid");
                    }
                })
                .Custom((info, context) =>
                {
                    if (Convert.ToString(info).Contains("00000000"))
                    {
                        context.AddFailure("The transaction card guid is invalid and contains 00000000");
                    }
                });
        }

        protected void ValidateAmountMinimum10Cents()
        {
            RuleFor(c => c.Amount)
                .Custom((info, context) =>
                {
                    if (info < 10)
                    {
                        context.AddFailure("The amount must have greater than 10 cents");
                    }
                });
        }

        protected void PasswordBetween4and6Digits()
        {
            RuleFor(c => new { c.HasPassword, c.Password })
                .Custom((info, context) =>
                {
                    // cartao com chip
                    if (Convert.ToBoolean(info.HasPassword))
                    {
                        if (StringCipher.Decrypt(info.Password, "StefanSilva@#@Stone##2019").Length < 4
                            || StringCipher.Decrypt(info.Password, "StefanSilva@#@Stone##2019").Length > 6)
                        {
                            context.AddFailure("Password between 4 and 6 digits");
                        }
                    }
                })
                .Custom((info, context) =>
                {
                    // cartao com chip
                    if (Convert.ToBoolean(info.HasPassword))
                    {
                        if (StringCipher.Decrypt(info.Password, "StefanSilva@#@Stone##2019").Length  == 0)
                        {
                            context.AddFailure("Password error size");
                        }
                    }
                });
        }
    }
}
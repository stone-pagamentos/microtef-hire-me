using System;
using Karnak.Domain.Commands;
using FluentValidation;
using Karnak.Domain.Common;

namespace Karnak.Domain.Validations
{
    public abstract class CardBrandValidation<T> : AbstractValidator<T> where T : CardBrandCommand
    {
        protected void ValidateName()
        {
            RuleFor(c => c.Name)
                .Custom((info, context) =>
                {
                    if (info.Length == 0)
                    {
                        context.AddFailure("The Name is Required");
                    }
                })
                .Length(2, 30).WithMessage("The Name must have between 2 and 30 characters");
        }

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
    }
}
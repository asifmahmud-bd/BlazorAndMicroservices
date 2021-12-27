using System;
using FluentValidation;
using Ordering.Application.Features.Oders.Commands;

namespace Ordering.Application.Features.Validator
{
    public class UpdateOrderCommandValidator : AbstractValidator<UpdateOrderCommand>
    {
        public UpdateOrderCommandValidator()
        {
            RuleFor(p => p.UserName)
                .NotEmpty().WithMessage("{UserName} is requered")
                .MaximumLength(50).WithMessage("{UserName} does not exid 50 character");

            RuleFor(p => p.EmailAddress)
               .NotEmpty().WithMessage("{EmailAddress} is requered");

            RuleFor(p => p.TotalPrice)
               .NotEmpty().WithMessage("{TotalPrice} is requered")
               .GreaterThan(0).WithMessage("{TotalPrice} must be greter then 0");
        }
    }
}

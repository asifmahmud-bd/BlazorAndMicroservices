using System;
using FluentValidation;
using Ordering.Application.Features.Oders.Commands;

namespace Ordering.Application.Features.Validator
{
    public class CheckoutOrderCommandValidator : AbstractValidator<CheckoutOrderCommand>
    {
        public CheckoutOrderCommandValidator()
        {
            RuleFor(p => p.UserName)
                .NotEmpty().WithMessage("{UserName} is requered")
                .NotNull()
                .MaximumLength(50).WithMessage("{UserName} must not exceed 50 charecter");

            RuleFor(p => p.EmailAddress)
                .NotNull().WithMessage("{EmailAddress} is requered");

            RuleFor(p => p.TotalPrice)
                .NotEmpty().WithMessage("{TotalPrice} is requered")
                .GreaterThan(0).WithMessage("{TotalPrice} should be greter then zero");
        }
    }
}

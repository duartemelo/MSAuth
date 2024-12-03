using FluentValidation;
using MSAuth.Domain.DTOs;
using MSAuth.Domain.Utils;

namespace MSAuth.Domain.Validators
{
    public class UserCreateDTOValidator : AbstractValidator<UserCreateDTO>
    {
        public UserCreateDTOValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("E-mail cannot be null.")
                .NotNull().WithMessage("E-mail cannot be empty.")
                .EmailAddress().WithMessage("E-mail must be valid.");
            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("First name cannot be null.")
                .NotNull().WithMessage("First name cannot be empty.");
            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Last name cannot be null.")
                .NotNull().WithMessage("Last name cannot be empty.");
            RuleFor(user => user.Password)
                .NotEmpty().WithMessage("The password must not be empty.")
                .NotNull().WithMessage("The password must not be null.")
                .MinimumLength(8).WithMessage("The password must have at least 8 characters.")
                .Must(PasswordValidation.ContainsUpperCaseLetter).WithMessage("The password must have at least 1 uppercase letter.")
                .Must(PasswordValidation.ContainsDigit).WithMessage("The password must have at least 1 number.");
        }
    }
}

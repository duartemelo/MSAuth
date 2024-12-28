using FluentValidation;
using MSGym.Domain.DTOs;

namespace MSGym.Domain.Validators
{
    public class GymCreateDTOValidator : AbstractValidator<GymCreateDTO>
    {
        public GymCreateDTOValidator() 
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name cannot be empty.")
                .NotNull().WithMessage("Name cannot be null.");
            RuleFor(x => x.Address)
                .NotEmpty().WithMessage("Address cannot be empty.")
                .NotNull().WithMessage("Address cannot be null.");
            RuleFor(x => x.ZipCode)
                .NotEmpty().WithMessage("Zipcode cannot be empty.")
                .NotNull().WithMessage("Zipcode cannoto be null.")
                .Matches(@"^\d{4}-\d{3}$").WithMessage("Zipcode must be in the format 0000-000.");
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email cannot be empty.")
                .NotNull().WithMessage("Email cannot be null.")
                .EmailAddress().WithMessage("Email must be valid.");
        }
    }
}

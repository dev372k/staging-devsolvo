using BL.Developer.DTOs.Request;
using FluentValidation;

namespace BL.Developer.Validators;
public class AddDeveloperValidator : AbstractValidator<AddDeveloperDto>
{
    public AddDeveloperValidator()
    {
        RuleFor(user => user.name)
            .NotEmpty().WithMessage("Name is required.")
            .Length(3, 20).WithMessage("Name must be between 3 and 20 characters.");

        RuleFor(user => user.surname)
            .NotEmpty().WithMessage("Surname is required.")
            .Length(3, 20).WithMessage("Surname must be between 3 and 20 characters.");
    }
}


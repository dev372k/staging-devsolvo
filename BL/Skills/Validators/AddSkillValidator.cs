using BL.Developer.DTOs.Request;
using BL.Skills.DTOs.Request;
using FluentValidation;

namespace BL.Developer.Validators;
public class AddSkillValidator : AbstractValidator<AddSkillDto>
{
    public AddSkillValidator()
    {
        RuleFor(user => user.name)
            .NotEmpty().WithMessage("Name is required.")
            .Length(3, 20).WithMessage("Name must be between 3 and 20 characters.");
    }
}

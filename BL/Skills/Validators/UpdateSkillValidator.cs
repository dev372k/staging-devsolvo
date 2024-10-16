using BL.Developer.DTOs.Request;
using BL.Skills.DTOs.Request;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Developer.Validators;

public class UpdateSkillValidator : AbstractValidator<UpdateSkillDto>
{
    public UpdateSkillValidator()
    {
        RuleFor(user => user.name)
            .NotEmpty().WithMessage("Name is required.")
            .Length(3, 20).WithMessage("Name must be between 3 and 20 characters.");

    }
}
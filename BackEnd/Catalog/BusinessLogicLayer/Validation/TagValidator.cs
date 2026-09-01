using BusinessLogicLayer.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Validation
{
    public class TagValidator : AbstractValidator<TagDTO>
    {
        public TagValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.Name).NotEmpty().WithMessage("Please write name")
                                                .Length(1, 100).WithMessage("Name should has minimum 1 symbol length and 100 symbol length")
                                                .Must(t => !string.IsNullOrWhiteSpace(t)).WithMessage("Please write name");
        }
    }
}

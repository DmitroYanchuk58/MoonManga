using BusinessLogicLayer.DTOs;
using FluentValidation;

namespace BusinessLogicLayer.Validation
{
    public class ChapterValidator : AbstractValidator<ChapterDTO>
    {
        public ChapterValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.Order)
                .Cascade(CascadeMode.Stop)
                .NotNull()
                .GreaterThan(0)
                .WithMessage("Order must be a positive number starting from 1.");
            RuleFor(x => x.Volume)
                .Cascade(CascadeMode.Stop)
                .NotNull()
                .GreaterThan(0)
                .WithMessage("Volume must be a positive number starting from 1.");

            RuleFor(x => x.Title).Length(0, 200).WithMessage("Title must be at most 200 characters long.");
        }
    }
}

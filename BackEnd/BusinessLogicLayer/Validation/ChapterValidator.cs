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
        }
    }
}

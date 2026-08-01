using BusinessLogicLayer.DTOs;
using FluentValidation;

namespace BusinessLogicLayer.Validation
{
    public class ChapterValidator : AbstractValidator<ChapterDTO>
    {
        public ChapterValidator()
        {
            RuleFor(x => x.Id).NotEmpty();

            RuleFor(x => x.Title).Length(0, 200).WithMessage("Title must be at most 200 characters long.");
        }
    }
}

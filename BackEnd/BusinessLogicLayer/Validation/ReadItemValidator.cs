using BusinessLogicLayer.DTOs;
using FluentValidation;

namespace BusinessLogicLayer.Validation
{
    public class ReadItemValidator : AbstractValidator<ReadItemDTO>
    {
        public ReadItemValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.Title).NotEmpty().WithMessage("Please write title")
                                                .Length(1,100).WithMessage("Title should has minimum 1 symbol length and 100 symbol length")
                                                .Must(t => !string.IsNullOrWhiteSpace(t)).WithMessage("Please write title");
            RuleFor(x => x.Type).NotEmpty().WithMessage("Please choose type of item")
                                                .Must(t => !string.IsNullOrWhiteSpace(t.ToString())).WithMessage("Please choose type of item")
                                                .IsInEnum().WithMessage("Please choose a valid type from the list."); 

        }
    }
}

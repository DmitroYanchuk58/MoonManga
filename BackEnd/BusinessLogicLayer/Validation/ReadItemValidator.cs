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

            RuleFor(x => x.Description).NotEmpty().WithMessage("Please write description")
                                                .Length(1, 100000).WithMessage("Description should has minimum 1 symbol length and maximum 100000 symbol length");
                                               
            RuleFor(x => x.Type).NotEmpty().WithMessage("Please choose type of item")
                                                .Must(t => !string.IsNullOrWhiteSpace(t.ToString())).WithMessage("Please choose type of item")
                                                .IsInEnum().WithMessage("Please choose a valid type from the list.");
            RuleFor(x => x.CoverImage)
                        .Cascade(CascadeMode.Stop)
                        .NotNull()
                        .WithMessage("Image data must not be null.")
                        .Must(image => image.Length > 0)
                        .WithMessage("Image data must not be empty.");
        }
    }
}

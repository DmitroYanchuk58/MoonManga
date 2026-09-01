using BusinessLogicLayer.DTOs;
using FluentValidation;

namespace BusinessLogicLayer.Validation
{
    public class PageValidator : AbstractValidator<PageDTO>
    {
        public PageValidator() 
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.Order)
                .Cascade(CascadeMode.Stop)
                .NotNull()
                .GreaterThan(0)
                .WithMessage("Order must be a positive number starting from 1.");
            RuleFor(x => x.Image)
                .Cascade(CascadeMode.Stop)
                .NotNull()
                .WithMessage("Image data must not be null.")
                .Must(image => image.Length > 0)
                .WithMessage("Image data must not be empty.");
        }
    }
}

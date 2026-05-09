using BusinessLogicLayer.DTOs;
using FluentValidation;

namespace BusinessLogicLayer.Validation
{
    public class ReadItemTagValidator : AbstractValidator<ReadItemTagDTO>
    {
        public ReadItemTagValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.IdReadItem).NotEmpty();
            RuleFor(x => x.IdTag).NotEmpty();
        }
    }
}

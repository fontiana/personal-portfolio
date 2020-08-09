using FluentValidation;
using PersonalPortfolio.Areas.Admin.Models;

namespace PersonalPortfolio.Areas.Admin.Validator
{
    public class ProductValidator : AbstractValidator<ProjectViewModel>
    {
        public ProductValidator()
        {
            RuleFor(x => x.Title).NotEmpty().WithMessage("Please specify a title");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Please specify a description");
            RuleFor(x => x.TechStack).NotEmpty().WithMessage("Please specify a tech stack");
        }
    }
}

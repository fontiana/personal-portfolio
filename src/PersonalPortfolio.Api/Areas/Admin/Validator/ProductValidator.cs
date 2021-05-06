using FluentValidation;
using PersonalPortfolio.Areas.Admin.Models;

namespace PersonalPortfolio.Areas.Admin.Validator
{
    public class ProjectValidator : AbstractValidator<ProjectViewModel>
    {
        public ProjectValidator()
        {
            RuleFor(x => x.Title).NotEmpty().WithMessage("Please specify a title");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Please specify a description");
            RuleFor(x => x.ShortDescription).NotEmpty().WithMessage("Please specify a short description");
            RuleFor(x => x.TechStack).NotEmpty().WithMessage("Please specify a tech stack");
            RuleFor(x => x.Image).NotNull().WithMessage("Please specify a showcase image");
        }
    }
}

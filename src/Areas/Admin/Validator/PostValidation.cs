using FluentValidation;
using PersonalPortfolio.Areas.Admin.Models;

namespace PersonalPortfolio.Areas.Admin.Validator
{
    public class PostValidator : AbstractValidator<PostViewModel>
    {
        public PostValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty()
                .WithMessage("Please specify a title");
            RuleFor(x => x.Description)
                .NotEmpty()
                .WithMessage("Please specify a description");
            RuleFor(x => x.Image)
                .NotNull()
                .NotEmpty()
                .WithMessage("Please specify a showcase image");
        }
    }
}

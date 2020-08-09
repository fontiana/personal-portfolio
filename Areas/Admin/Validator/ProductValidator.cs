
using FluentValidation;
using Personal.Portfolio.Areas.Admin.Models;

namespace PersonalPortfolio.Areas.Admin.Validator
{
    public class ProductValidator : AbstractValidator<ProjectViewModel>
    {
        public ProductValidator()
        {

        }
    }
}

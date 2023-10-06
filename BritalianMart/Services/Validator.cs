using BritalianMart.Models;
using FluentValidation;

namespace BritalianMart.Services
{

    public class ProductValidator : AbstractValidator<ProductModel>
    {
        public ProductValidator() {

            RuleFor(x => x.Description).MinimumLength(5);
            RuleFor(x => x.Price).GreaterThanOrEqualTo(0);
            RuleFor(x => x.Plu).MinimumLength(5).NotNull();
            RuleFor(x => x.Category).MinimumLength(3).NotNull();
            RuleFor(x => x.Brand).MinimumLength(3);

        }


    }
   
}

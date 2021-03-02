using FluentValidation;
using OpenFoodFacts.Application.Product.ViewModels;

namespace OpenFoodFacts.Application.Product.Commands.Update
{
    class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
    {
        public UpdateProductCommandValidator()
        {
            RuleFor(x => x.Code).NotEmpty().NotNull();
            RuleFor(x => x.Product).NotNull();
            RuleFor(x => x.Product).SetValidator(new ProductValidator());
        }
    }

    public class ProductValidator : AbstractValidator<ProductViewModel>
    {
        public ProductValidator()
        {
            RuleFor(x => x.Status).IsInEnum();
            RuleFor(x => x.Url).MaximumLength(600);
            RuleFor(x => x.Creator).MaximumLength(100);
            RuleFor(x => x.Created_t).MaximumLength(100);
            RuleFor(x => x.Last_modified_t).MaximumLength(200);
            RuleFor(x => x.Product_name).MaximumLength(200);
            RuleFor(x => x.Quantity).MaximumLength(100);
            RuleFor(x => x.Brands).MaximumLength(100);
            RuleFor(x => x.Categories).MaximumLength(100);
            RuleFor(x => x.Labels).MaximumLength(200);
            RuleFor(x => x.Cities).MaximumLength(200);
            RuleFor(x => x.Purchase_places).MaximumLength(200);
            RuleFor(x => x.Stores).MaximumLength(200);
            RuleFor(x => x.Ingredients_Text).MaximumLength(800);
            RuleFor(x => x.Traces).MaximumLength(200);
            RuleFor(x => x.Serving_Size).MaximumLength(200);
            RuleFor(x => x.Nutriscore_Grade).MaximumLength(200);
            RuleFor(x => x.Main_Category).MaximumLength(200);
            RuleFor(x => x.Image_Url).MaximumLength(200);
        }
    }
}


using FluentValidation;

namespace OpenFoodFacts.Application.Product.Queries.Get
{
    public class GetProductQueryValidator : AbstractValidator<GetProductQuery>
    {
        public GetProductQueryValidator()
        {
            RuleFor(x => x.Code).NotEmpty().NotNull();
        }
    }
}

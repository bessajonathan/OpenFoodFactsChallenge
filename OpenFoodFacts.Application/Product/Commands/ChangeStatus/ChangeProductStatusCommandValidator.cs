using FluentValidation;

namespace OpenFoodFacts.Application.Product.Commands.ChangeStatus
{
    public class ChangeProductStatusCommandValidator : AbstractValidator<ChangeProductStatusCommand>
    {
        public ChangeProductStatusCommandValidator()
        {
            RuleFor(x => x.Code).NotEmpty().NotNull();
        }
    }
}

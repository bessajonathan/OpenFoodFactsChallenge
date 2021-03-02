using MediatR;

namespace OpenFoodFacts.Application.Product.Commands.ChangeStatus
{
    public class ChangeProductStatusCommand : IRequest<Unit>
    {
        public string Code { get; set; }
    }
}

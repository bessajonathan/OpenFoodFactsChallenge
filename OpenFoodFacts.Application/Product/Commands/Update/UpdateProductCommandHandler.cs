using MediatR;
using OpenFoodFacts.Application.Service.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace OpenFoodFacts.Application.Product.Commands.Update
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand,Unit>
    {
        private readonly IOpenFoodApplicationService _openApplicationService;

        public UpdateProductCommandHandler(IOpenFoodApplicationService openApplicationService)
        {
            _openApplicationService = openApplicationService;
        }
        public async Task<Unit> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            await _openApplicationService.UpdateProduct(request.Product, request.Code);

            return Unit.Value;
        }
    }
}

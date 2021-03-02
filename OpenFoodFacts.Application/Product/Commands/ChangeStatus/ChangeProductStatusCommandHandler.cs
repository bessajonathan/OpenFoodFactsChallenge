using MediatR;
using OpenFoodFacts.Application.Service.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace OpenFoodFacts.Application.Product.Commands.ChangeStatus
{
    public class ChangeProductStatusCommandHandler: IRequestHandler<ChangeProductStatusCommand,Unit>
    {
        private readonly IOpenFoodApplicationService _applicationService;

        public ChangeProductStatusCommandHandler(IOpenFoodApplicationService applicationService)
        {
            _applicationService = applicationService;
        }
        public async Task<Unit> Handle(ChangeProductStatusCommand request, CancellationToken cancellationToken)
        {
            await _applicationService.ChangeProductStatus(request.Code);

            return Unit.Value;
        }
    }
}

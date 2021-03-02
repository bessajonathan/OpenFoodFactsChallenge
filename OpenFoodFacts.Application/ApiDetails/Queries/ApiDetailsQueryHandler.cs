using MediatR;
using OpenFoodFacts.Application.ApiDetails.ViewModels;
using OpenFoodFacts.Application.Service.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace OpenFoodFacts.Application.ApiDetails.Queries
{
    public class ApiDetailsQueryHandler : IRequestHandler<ApiDetailsQuery,ApiDetailsViewModel>
    {
        private readonly IApiApplicationService _apiApplicationService;

        public ApiDetailsQueryHandler(IApiApplicationService apiApplicationService)
        {
            _apiApplicationService = apiApplicationService;
        }
        public async Task<ApiDetailsViewModel> Handle(ApiDetailsQuery request, CancellationToken cancellationToken)
        {
            return await _apiApplicationService.GetApiDetails();
        }
    }
}

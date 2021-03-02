using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using OpenFoodFacts.Application.Product.ViewModels;
using OpenFoodFacts.Application.Service.Interfaces;
using OpenFoodFacts.Domain.Service.Interfaces;

namespace OpenFoodFacts.Application.Product.Queries.Get
{
    public class GetProductQueryHandler:IRequestHandler<GetProductQuery,ProductViewModel>
    {
        private readonly IOpenFoodApplicationService _openFoodApplicationService;

        public GetProductQueryHandler(IOpenFoodApplicationService openFoodApplicationService)
        {
            _openFoodApplicationService = openFoodApplicationService;
        }
        public async Task<ProductViewModel> Handle(GetProductQuery request, CancellationToken cancellationToken)
        {
            return await _openFoodApplicationService.GetProductByCode(request.Code);
        }
    }
}

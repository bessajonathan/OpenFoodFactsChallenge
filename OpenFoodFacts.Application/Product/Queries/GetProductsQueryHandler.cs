using AutoMapper;
using MediatR;
using OpenFoodFacts.Application.Infra;
using OpenFoodFacts.Application.Product.ViewModels;
using OpenFoodFacts.Application.Service.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;
using X.PagedList;

namespace OpenFoodFacts.Application.Product.Queries
{
    public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, ListViewModel<IEnumerable<ProductViewModel>>>
    {
        private readonly IOpenFoodApplicationService _openFoodApplicationService;
        private readonly IMapper _mapper;

        public GetProductsQueryHandler(IOpenFoodApplicationService openFoodApplicationService)
        {
            _openFoodApplicationService = openFoodApplicationService;
        }
        public async Task<ListViewModel<IEnumerable<ProductViewModel>>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {
            var data = await _openFoodApplicationService.GetProducts().Result.AsQueryable().OrderBy(request.OrderBy)
                .ToPagedListAsync(request.Page, request.PageSize);

            return new ListViewModel<IEnumerable<ProductViewModel>>
            {
                Data = data,
                TotalItemCount = data.TotalItemCount,
                HasNextPage = data.HasNextPage
            };
        }
    }
}

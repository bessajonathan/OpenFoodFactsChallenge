using System.Collections.Generic;
using MediatR;
using OpenFoodFacts.Application.Infra;
using OpenFoodFacts.Application.Product.ViewModels;

namespace OpenFoodFacts.Application.Product.Queries
{
    public class GetProductsQuery : QueryBase,IRequest<ListViewModel<IEnumerable<ProductViewModel>>>
    {
    }
}

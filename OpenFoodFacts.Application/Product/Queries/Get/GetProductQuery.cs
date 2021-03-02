using MediatR;
using OpenFoodFacts.Application.Product.ViewModels;

namespace OpenFoodFacts.Application.Product.Queries.Get
{
    public class GetProductQuery : IRequest<ProductViewModel>
    {
        public string Code { get; set; }
    }
}

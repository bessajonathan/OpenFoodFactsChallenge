using MediatR;
using Newtonsoft.Json;
using NSwag.Annotations;
using OpenFoodFacts.Application.Product.ViewModels;

namespace OpenFoodFacts.Application.Product.Commands.Update
{
    public class UpdateProductCommand : IRequest<Unit>
    {
        [JsonIgnore]
        [OpenApiIgnore]
        public string Code { get; set; }
        public ProductViewModel Product { get; set; }
    }
}

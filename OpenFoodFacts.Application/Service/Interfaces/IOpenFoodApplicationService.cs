using System.Collections.Generic;
using System.Threading.Tasks;
using OpenFoodFacts.Application.Product.ViewModels;

namespace OpenFoodFacts.Application.Service.Interfaces
{
    public interface IOpenFoodApplicationService
    {
        Task<IEnumerable<ProductViewModel>> GetProducts();
        Task<ProductViewModel> GetProductByCode(string code);
        Task ChangeProductStatus(string code);
        Task UpdateProduct(ProductViewModel productVm, string code);
    }
}

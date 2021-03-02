using System.Collections.Generic;
using System.Threading.Tasks;
using OpenFoodFacts.Domain.Entities;

namespace OpenFoodFacts.Domain.Service.Interfaces
{
    public interface IOpenFoodService
    {
        Task<bool> VerifyWriteStatus();
        Task<bool> VerifyReaderStatus();
        Task<IEnumerable<Product>> GetProducts();
        Task<Product> GetProductByCode(string code);
        Task ChangeProductStatus(Product product);
        Task UpdateProduct(Product product);
    }
}

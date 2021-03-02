using System.Collections.Generic;
using System.Threading.Tasks;
using OpenFoodFacts.Domain.Entities;
using OpenFoodFacts.Domain.Repository.Interfaces;
using OpenFoodFacts.Domain.Service.Interfaces;

namespace OpenFoodFacts.Domain.Service
{
    public class OpenFoodService : IOpenFoodService
    {
        private readonly IOpenFoodRepository _openFoodRepository;

        public OpenFoodService(IOpenFoodRepository openFoodRepository)
        {
            _openFoodRepository = openFoodRepository;
        }
        public async Task<bool> VerifyWriteStatus()
        {
            return await _openFoodRepository.VerifyWriteStatus();
        }

        public async Task<bool> VerifyReaderStatus()
        {
            return await _openFoodRepository.VerifyReaderStatus();
        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
            return await _openFoodRepository.GetProducts();
        }

        public async Task<Product> GetProductByCode(string code)
        {
            return await _openFoodRepository.GetProductByCode(code);
        }

        public async Task ChangeProductStatus(Product product)
        {
            product.SetTrashStatus();

            await _openFoodRepository.UpdateProduct(product);
        }

        public async Task UpdateProduct(Product product)
        {
            await _openFoodRepository.UpdateProduct(product);
        }
    }
}

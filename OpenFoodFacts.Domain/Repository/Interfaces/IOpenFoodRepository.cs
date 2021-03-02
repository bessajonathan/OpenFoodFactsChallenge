using OpenFoodFacts.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OpenFoodFacts.Domain.Repository.Interfaces
{
    public interface IOpenFoodRepository
    {
        void AddNewProducts(List<Product> products);
        void AddFileDownloaded(FileHistory fileHistory);
        Task<FileHistory> GetFileHistory(string fileName);
        void UpdateFileHistory(FileHistory fileHistory);
        Task<bool> VerifyWriteStatus();
        Task<bool> VerifyReaderStatus();
        Task<IEnumerable<Product>> GetProducts();
        Task<Product> GetProductByCode(string code);
        Task UpdateProduct(Product product);
    }
}

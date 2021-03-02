using System;
using Microsoft.EntityFrameworkCore;
using OpenFoodFacts.Domain.Entities;
using OpenFoodFacts.Domain.Repository.Interfaces;
using OpenFoodFacts.Persistence.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OpenFoodFacts.Persistence.Repository
{
    public class OpenFoodRepository : IOpenFoodRepository
    {
        private readonly IOpenFoodFactsContext _context;

        public OpenFoodRepository(IOpenFoodFactsContext context)
        {
            _context = context;
        }

        public void AddNewProducts(List<Product> products)
        {
            _context.Products.AddRange(products);
            _context.SaveChanges();
        }

        public void AddFileDownloaded(FileHistory fileHistory)
        {
            _context.FileHistorys.Add(fileHistory);
            _context.SaveChanges();
        }

        public async Task<FileHistory> GetFileHistory(string fileName)
        {
            return await _context.FileHistorys.FirstOrDefaultAsync(x => x.Name == fileName);
        }

        public void UpdateFileHistory(FileHistory fileHistory)
        {
            _context.FileHistorys.Update(fileHistory);
            _context.SaveChanges();
        }

        public async Task<bool> VerifyWriteStatus()
        {
            bool status = true;

            try
            {
                var fileHistoryTest = new FileHistory("Teste escrita", 0, 0);
                await _context.FileHistorys.AddAsync(fileHistoryTest);
                await _context.SaveChangesAsync();

                _context.FileHistorys.Remove(fileHistoryTest);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                status = false;
            }

            return status;
        }

        public async Task<bool> VerifyReaderStatus()
        {
            bool status = true;

            try
            {
                await _context.FileHistorys.FirstOrDefaultAsync();

            }
            catch (Exception)
            {
                status = false;
            }

            return status;
        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<Product> GetProductByCode(string code)
        {
            return await _context.Products.FirstOrDefaultAsync(x => x.Code == long.Parse(code));
        }

        public async Task UpdateProduct(Product product)
        {
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
        }
    }
}

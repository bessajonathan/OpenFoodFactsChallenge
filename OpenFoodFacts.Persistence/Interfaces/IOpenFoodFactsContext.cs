using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OpenFoodFacts.Domain.Entities;

namespace OpenFoodFacts.Persistence.Interfaces
{
    public interface IOpenFoodFactsContext
    {
        DbSet<FileHistory> FileHistorys { get; set; }
        DbSet<Product> Products { get; set; }
        int SaveChanges();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}

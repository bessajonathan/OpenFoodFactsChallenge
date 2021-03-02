using Microsoft.EntityFrameworkCore;
using OpenFoodFacts.Domain.Entities;
using OpenFoodFacts.Persistence.Interfaces;

namespace OpenFoodFacts.Persistence.Context
{
    public class OpenFoodFactsContext : DbContext, IOpenFoodFactsContext
    {
        public OpenFoodFactsContext(DbContextOptions<OpenFoodFactsContext> options) : base(options) { }

        public DbSet<FileHistory> FileHistorys { get; set; }
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(OpenFoodFactsContext).Assembly);
        }
    }
}

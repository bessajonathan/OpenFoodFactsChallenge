using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OpenFoodFacts.Domain.Entities;

namespace OpenFoodFacts.Persistence.Configurations
{
    public class FileHistoryConfiguration : IEntityTypeConfiguration<FileHistory>
    {
        public void Configure(EntityTypeBuilder<FileHistory> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).IsRequired().HasMaxLength(200);
            builder.Property(x => x.LinesRead).IsRequired();
            builder.Property(x => x.TotalLines).IsRequired();
        }
    }
}

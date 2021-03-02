using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using OpenFoodFacts.Domain.Entities;
using OpenFoodFacts.Domain.Enums;

namespace OpenFoodFacts.Persistence.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Code).IsRequired();
            builder.Property(x => x.Status).IsRequired().HasConversion(new EnumToStringConverter<EProductStatus>());
            builder.Property(x => x.Imported_t).IsRequired();
            builder.Property(x => x.Url).HasMaxLength(600);
            builder.Property(x => x.Creator).HasMaxLength(100);
            builder.Property(x => x.Created_t).HasMaxLength(100);
            builder.Property(x => x.Last_modified_t).HasMaxLength(200);
            builder.Property(x => x.Product_name).HasMaxLength(200);
            builder.Property(x => x.Quantity).HasMaxLength(100);
            builder.Property(x => x.Brands).HasMaxLength(100);
            builder.Property(x => x.Categories).HasMaxLength(100);
            builder.Property(x => x.Labels).HasMaxLength(200);
            builder.Property(x => x.Cities).HasMaxLength(200);
            builder.Property(x => x.Purchase_places).HasMaxLength(200);
            builder.Property(x => x.Stores).HasMaxLength(200);
            builder.Property(x => x.Ingredients_Text).HasMaxLength(800);
            builder.Property(x => x.Traces).HasMaxLength(200);
            builder.Property(x => x.Serving_Size).HasMaxLength(200);
            builder.Property(x => x.Serving_Quantity).HasColumnType("double(14,2)");
            builder.Property(x => x.Nutriscore_Score).HasColumnType("double(14,2)");
            builder.Property(x => x.Nutriscore_Grade).HasMaxLength(200);
            builder.Property(x => x.Main_Category).HasMaxLength(200);
            builder.Property(x => x.Image_Url).HasMaxLength(200);
        }
    }
}

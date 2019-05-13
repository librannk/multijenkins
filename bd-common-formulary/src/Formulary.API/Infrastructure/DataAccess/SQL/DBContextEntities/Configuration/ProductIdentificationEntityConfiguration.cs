using Formulary.API.Infrastructure.DataAccess.SQL.DBContextEntities.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Formulary.API.Infrastructure.DataAccess.SQL.DBContextEntities.Configuration
{
    /// <summary>
    /// ProductIdentificationEntity Configuration
    /// </summary>
    public class ProductIdentificationEntityConfiguration : IEntityTypeConfiguration<ProductIdentificationEntity>
    {
        /// <summary>
        /// Configuration between Entities
        /// </summary>
        /// <param name="builder"></param>
        public void Configure(EntityTypeBuilder<ProductIdentificationEntity> builder)
        {
            builder.ToTable("ProductID");

            builder.HasOne<MedicationItemEntity>(s => s.MedicationItem)
           .WithMany(g => g.ProductIdentifications)
           .HasForeignKey(s => s.ItemKey);

            builder.HasOne<ItemEntity>(s => s.Item)
           .WithMany(g => g.ProductIdentifications)
           .HasForeignKey(s => s.ItemKey);
        }


    }
}

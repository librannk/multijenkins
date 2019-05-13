using Formulary.API.Infrastructure.DataAccess.SQL.DBContextEntities.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Formulary.API.Infrastructure.DataAccess.SQL.DBContextEntities.Configuration
{
    /// <summary>
    /// FormularyEntity Configuration
    /// </summary>
    public class ProductIDRecommendedItemEntityConfiguration : IEntityTypeConfiguration<ProductIDRecommendedItemEntity>
    {
        /// <summary>
        /// Configure
        /// </summary>
        /// <param name="builder"></param>
        public void Configure(EntityTypeBuilder<ProductIDRecommendedItemEntity> builder)
        {
            builder.ToTable("ProductIDRecommendedItem");

            // builder.HasOne<ProductIdentificationEntity>(s => s.ProductIdentification)
            //.WithMany(g => g.ProductIDRecommendedItems)
            //.HasForeignKey(s => s.ResultingProductIdentificationKey);

            // builder.HasOne<RecommendedItemResultEntity>(s => s.RecommendedItemResult)
            //.WithMany(g => g.ProductIDRecommendedItems)
            //.HasForeignKey(s => s.RecommendedItemResultInternalCode);

            // builder.HasOne<ItemEntity>(s => s.Item)
            //.WithMany(g => g.ProductIDRecommendedItems)
            //.HasForeignKey(s => s.RecommendedItemKey);

            // builder.HasOne<MedicationItemEntity>(s => s.MedicationItem)
            //.WithMany(g => g.ProductIDRecommendedItems)
            //.HasForeignKey(s => s.RecommendedMedicationItemKey);


        }
    }
}

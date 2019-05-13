using Formulary.API.Infrastructure.DataAccess.SQL.DBContextEntities.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Formulary.API.Infrastructure.DataAccess.SQL.DBContextEntities.Configuration
{
    /// <summary>
    /// RecommendedItemResultEntity Configuration
    /// </summary>
    public class MedicationItemEntityConfiguration : IEntityTypeConfiguration<MedicationItemEntity>
    {
        /// <summary>
        /// Configure Method
        /// </summary>
        /// <param name="builder"></param>
        public void Configure(EntityTypeBuilder<MedicationItemEntity> builder)
        {
            builder.ToTable("MedItem");

            builder.HasOne<ItemEntity>(s => s.Item)
           .WithOne(g => g.MedicationItem)
           .HasForeignKey<MedicationItemEntity>(s => s.MedicationItemKey);

            builder.HasOne<DispensingFormLookupEntity>(s => s.dispensingFormLookup)
           .WithMany(g => g.MedicationItems)
           .HasForeignKey(s => s.DispenseFormLookupKey);

            builder.HasOne<DispensingUnitLookupEntity>(s => s.dispensingUnitLookup)
           .WithMany(g => g.MedicationItems)
           .HasForeignKey(s => s.DispenseUnitLookupKey);

            builder.HasOne<GLAccountEntity>(s => s.GLAccount)
           .WithMany(g => g.MedicationItems)
           .HasForeignKey(s => s.GLAccountKey);

            builder.HasOne<MedClassEntity>(s => s.MedClassEntity)
           .WithMany(g => g.MedicationItems)
           .HasForeignKey(s => s.MedicationClassKey);

        }
    }
}

using Formulary.API.Infrastructure.DataAccess.SQL.DBContextEntities.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Formulary.API.Infrastructure.DataAccess.SQL.DBContextEntities.Configuration
{
    /// <summary>
    /// Relationship configuration with FacilityItemEntity
    /// </summary>
    public class FacilityItemEntityConfiguration : IEntityTypeConfiguration<FacilityItemEntity>
    {
        /// <summary>
        /// configure method
        /// </summary>
        /// <param name="builder"></param>
        public void Configure(EntityTypeBuilder<FacilityItemEntity> builder)
        {
            builder.ToTable("FacilityItem");

            builder.HasOne<UomEntity>(s => s.UomEntity)
           .WithMany(g => g.FacilityItems)
           .HasForeignKey(s => s.UOMKey);

            builder.HasOne<ItemEntity>(s => s.ItemEntity)
           .WithMany(g => g.FacilityItems)
           .HasForeignKey(s => s.ItemKey);

        }
    }
}

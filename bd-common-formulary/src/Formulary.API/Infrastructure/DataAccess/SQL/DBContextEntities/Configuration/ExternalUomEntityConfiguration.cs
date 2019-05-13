using Formulary.API.Infrastructure.DataAccess.SQL.DBContextEntities.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Formulary.API.Infrastructure.DataAccess.SQL.DBContextEntities.Configuration
{
    /// <summary>
    /// ExternalUomEntity configuration
    /// </summary>
    public class ExternalUomEntityConfiguration : IEntityTypeConfiguration<ExternalUomEntity>
    {
        /// <summary>
        /// Configure method
        /// </summary>
        /// <param name="builder"></param>
        public void Configure(EntityTypeBuilder<ExternalUomEntity> builder)
        {
            builder.ToTable("ExternalUOM");

            builder.HasOne<UomEntity>(s => s.UomEntity)
           .WithMany(g => g.ExternalUoms)
           .HasForeignKey(s => s.UOMKey);


        }
    }
}

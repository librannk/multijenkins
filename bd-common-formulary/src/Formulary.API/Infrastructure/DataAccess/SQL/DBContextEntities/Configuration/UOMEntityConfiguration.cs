using Formulary.API.Infrastructure.DataAccess.SQL.DBContextEntities.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Formulary.API.Infrastructure.DataAccess.SQL.DBContextEntities.Configuration
{
    /// <summary>
    /// UomEntity Configuration.
    /// Implements the <see cref="Microsoft.EntityFrameworkCore.IEntityTypeConfiguration{Formulary.API.Infrastructure.DataAccess.SQL.DBContextEntities.Entities.UomEntity}" />
    /// </summary>
    /// <seealso cref="Microsoft.EntityFrameworkCore.IEntityTypeConfiguration{Formulary.API.Infrastructure.DataAccess.SQL.DBContextEntities.Entities.UomEntity}" />
    public class UomEntityConfiguration : IEntityTypeConfiguration<UomEntity>
    {
        /// <summary>
        /// Configures the entity of type <typeparamref name="TEntity" />.
        /// </summary>
        /// <param name="builder">The builder to be used to configure the entity type.</param>
        public void Configure(EntityTypeBuilder<UomEntity> builder)
        {
            builder.ToTable("UOM");

            builder.HasOne<UomEntity>(s => s.Uom)
           .WithMany(g => g.Uoms)
           .HasForeignKey(s => s.BaseUomKey);


        }
    }
}

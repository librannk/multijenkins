using Formulary.API.Infrastructure.DataAccess.SQL.DBContextEntities.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Formulary.API.Infrastructure.DataAccess.SQL.DBContextEntities.Configuration
{
    /// <summary>
    /// ItemEntityConfiguration Configuration
    /// </summary>
    public class ItemEntityConfiguration : IEntityTypeConfiguration<ItemEntity>
    {
        /// <summary>
        /// Configure Method
        /// </summary>
        /// <param name="builder"></param>
        public void Configure(EntityTypeBuilder<ItemEntity> builder)
        {
            builder.ToTable("Item");
        }
    }
}

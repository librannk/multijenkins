using Formulary.API.Infrastructure.DataAccess.SQL.DBContextEntities.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Formulary.API.Infrastructure.DataAccess.SQL.DBContextEntities.Configuration
{
    /// <summary>
    /// RecommendedItemResultEntity Configuration
    /// </summary>
    public class RecommendedItemResultEntityConfiguration : IEntityTypeConfiguration<RecommendedItemResultEntity>
    {
        /// <summary>
        /// Configure RecommendedItemResultEntity
        /// </summary>
        /// <param name="builder"></param>
        public void Configure(EntityTypeBuilder<RecommendedItemResultEntity> builder)
        {
            builder.ToTable("RecommendedItemResult");
        }
    }
}

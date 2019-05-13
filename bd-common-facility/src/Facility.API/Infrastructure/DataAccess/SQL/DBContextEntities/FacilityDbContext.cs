using Facility.API.Infrastructure.DataAccess.SQL.DBContextEntities.Entities;
using Microsoft.EntityFrameworkCore;

namespace Facility.API.Infrastructure.DataAccess.SQL.DBContextEntities
{
    /// <summary>
    /// 
    /// </summary>
    public class FacilityDbContext : DbContext
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="FacilityDbContext"/> class.
        /// </summary>
        public FacilityDbContext()
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FacilityDbContext"/> class.
        /// </summary>
        /// <param name="options">The options.</param>
        public FacilityDbContext(DbContextOptions<FacilityDbContext> options)
            : base(options)
        {

        }

        /// <summary>
        /// Gets or sets the facilities Table.
        /// </summary>
        /// <value>The facilities.</value>
        public DbSet<FacilityEntity> Facilities { get; set; }
        /// <summary>
        /// Gets or sets the ControlledSubstanceLicenses Table.
        /// </summary>
        /// <value>The controlled substance licenses.</value>
        public DbSet<ControlledSubstanceLicenseEntity> ControlledSubstanceLicenses { get; set; }
        /// <summary>
        /// Gets or sets the ControlledSubstanceLicenseFacilities  Table.
        /// </summary>
        /// <value>The controlled substance license facilities.</value>
        public DbSet<ControlledSubstanceLicenseFacilityEntity> ControlledSubstanceLicenseFacilities { get; set; }

        /// <summary>
        /// Override this method to further configure the model that was discovered by convention from the entity types
        /// exposed in <see cref="T:Microsoft.EntityFrameworkCore.DbSet`1" /> properties on your derived context. The resulting model may be cached
        /// and re-used for subsequent instances of your derived context.
        /// </summary>
        /// <param name="modelBuilder">The builder being used to construct the model for this context. Databases (and other extensions) typically
        /// define extension methods on this object that allow you to configure aspects of the model that are specific
        /// to a given database.</param>
        /// <remarks>If a model is explicitly set on the options for this context (via <see cref="M:Microsoft.EntityFrameworkCore.DbContextOptionsBuilder.UseModel(Microsoft.EntityFrameworkCore.Metadata.IModel)" />)
        /// then this method will not be run.</remarks>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Unique constraint cannot be set via attributes in aspNetCore
            modelBuilder.Entity<FacilityEntity>(entity => { entity.HasIndex(b => b.FacilityCode).IsUnique(); });
        }
    }
}
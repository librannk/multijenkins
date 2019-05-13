using System;
using Formulary.API.Infrastructure.DataAccess.SQL.DBContextEntities.Configuration;
using Formulary.API.Infrastructure.DataAccess.SQL.DBContextEntities.Entities;
using Microsoft.EntityFrameworkCore;

namespace Formulary.API.Infrastructure.DataAccess.SQL.DBContextEntities
{
    /// <summary>
    /// ApplicationDBContext is class  which represent DB in memory.
    /// </summary>
    public class ApplicationDBContext : DbContext
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public ApplicationDBContext()
        {

        }
        /// <summary>
        /// Constructor with db context parameters
        /// </summary>
        /// <param name="options"></param>
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {

        }
        /// <summary>
        /// Method that called on configuring 
        /// </summary>
        /// <param name="optionsBuilder"></param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
        /// <summary>
        /// Formualry entities
        /// </summary>
        public DbSet<FormularyEntity> FormualryEntities { get; set; }
        /// <summary>
        /// Facility entities
        /// </summary>
        public DbSet<FacilityEntity> FacilityEntities { get; set; }
        /// <summary>
        /// NDC entities
        /// </summary>
       public DbSet<NDCEntity> NDCEntities { get; set; }
        /// <summary>
        /// NDC entities
        /// </summary>
        public DbSet<FacilityNDCAssocEntity> FacilityNDCAssocEntities { get; set; }
        /// <summary>
        /// ProductIdentificationEntity
        /// </summary>
        public DbSet<ProductIdentificationEntity> ProductIdentificationEntities { get; set; }
        /// <summary>
        /// ProductIDRecommendedItems
        /// </summary>
        public DbSet<ProductIDRecommendedItemEntity> ProductIDRecommendedItems { get; set; }
        /// <summary>
        /// RecommendedItemResults
        /// </summary>
        public DbSet<RecommendedItemResultEntity> RecommendedItemResults { get; set; }
        /// <summary>
        /// MedicationItems
        /// </summary>
        public DbSet<MedicationItemEntity> MedicationItems { get; set; }
        /// <summary>
        /// Items
        /// </summary>
        public DbSet<ItemEntity> Items { get; set; }
        /// <summary>
        /// DispensingFormLookups
        /// </summary>
        public DbSet<DispensingFormLookupEntity> DispensingFormLookups { get; set; }
        /// <summary>
        /// DispensingUnitLookups
        /// </summary>
        public DbSet<DispensingUnitLookupEntity> DispensingUnitLookups { get; set; }
        /// <summary>
        /// GLAccounts
        /// </summary>
        public DbSet<GLAccountEntity> GLAccounts { get; set; }
        /// <summary>
        /// Gets or sets the formulary standards.
        /// </summary>
        /// <value>The formulary standards.</value>
        public DbSet<FormularyStandard> FormularyStandards { get; set; }


        /// <summary>
        /// Preferred Ordering
        /// </summary>
        /// <value>The preferred ordering.</value>
        public DbSet<PreferredOrderingEntity> PreferredOrdering{ get; set; }

        /// <summary>
        /// OnModelCreating Method
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ItemEntityConfiguration());
            modelBuilder.ApplyConfiguration(new ProductIDRecommendedItemEntityConfiguration());
            modelBuilder.ApplyConfiguration(new MedicationItemEntityConfiguration());
            modelBuilder.ApplyConfiguration(new ProductIdentificationEntityConfiguration());
            modelBuilder.ApplyConfiguration(new RecommendedItemResultEntityConfiguration());
            modelBuilder.ApplyConfiguration(new ExternalUomEntityConfiguration());
            modelBuilder.ApplyConfiguration(new FacilityItemEntityConfiguration());
            modelBuilder.ApplyConfiguration(new UomEntityConfiguration());
        }
    }
}

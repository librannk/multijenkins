using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BD.Core.ElasticClient.SQL;
using Microsoft.EntityFrameworkCore;
using SiteConfiguration.API.FacilityConfiguration.Models;
using SiteConfiguration.API.Printers.Models.Data;
using SiteConfiguration.API.RoutingRules.Models;
using SiteConfiguration.API.Schedule.Models;
using SiteConfiguration.API.TransactionPriority.Models;
using BD.Core.ElasticClient.SQL;

namespace SiteConfiguration.API.Infrastructure.DataAccess.SQL.DBContextEntities
{
    /// <summary>
    /// ApplicationDBContext is class  which represent DB in memory.
    /// </summary>
    public class ApplicationDbContext : ElasticDbContext
    {
        /// <summary>
        /// 
        /// </summary>
        public ApplicationDbContext(IServiceProvider serviceProvider, DbContextOptions options) : base(serviceProvider, options)
        {

        }        
      
        public virtual DbSet<RoutingRule> RoutingRule { get; set; }
        public virtual DbSet<RoutingRuleDestination> RoutingRuleDestination { get; set; }
        public virtual DbSet<RoutingRuleScheduleTiming> RoutingRuleScheduleTiming { get; set; }
        public virtual DbSet<RoutingRuleTranPriority> RoutingRuleTranPriority { get; set; }
        public virtual DbSet<ScheduleTiming> ScheduleTiming { get; set; }
        public virtual DbSet<SiteConfiguration.API.TransactionPriority.Models.TransactionPriority> TransactionPriorities { get; set; }
        public virtual DbSet<Printer> Printer { get; set; }
        public virtual DbSet<PrinterModel> PrinterModel { get; set; }
        public virtual DbSet<FacilityLogisticsConfig> FacilityLogisticsConfig { get; set; }
        public virtual DbSet<SmartSort> SmartSort { get; set; }
        public virtual DbSet<SmartSortColumn> SmartSortColumn { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
                modelBuilder.Entity<RoutingRule>(entity =>
            {
                entity.HasKey(e => e.RoutingRuleKey)
                    .HasName("PK__RoutingR__0FB1C17FA55401AA");

                entity.Property(e => e.RoutingRuleKey).ValueGeneratedNever();
                entity.Property(e => e.RoutingRuleName)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<RoutingRuleDestination>(entity =>
            {
                entity.HasKey(e => e.RoutingRuleDestinationKey)
                    .HasName("PK__RoutingR__E19F25F328C0BF07");

                entity.Property(e => e.RoutingRuleDestinationKey).ValueGeneratedNever();
                //entity.HasOne(d => d.DestinationKeyNavigation)
                //    .WithMany(p => p.RoutingRuleDestination)
                //    .HasForeignKey(d => d.DestinationKey)
                //    .OnDelete(DeleteBehavior.ClientSetNull)
                //    .HasConstraintName("FK__RoutingRu__Desti__114A936A");

                entity.HasOne(d => d.RoutingRuleKeyNavigation)
                    .WithMany(p => p.RoutingRuleDestination)
                    .HasForeignKey(d => d.RoutingRuleKey)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__RoutingRu__Routi__123EB7A3");
            });

            modelBuilder.Entity<RoutingRuleScheduleTiming>(entity =>
            {
                entity.HasKey(e => e.RoutingRuleScheduleTimingKey)
                    .HasName("PK__RoutingR__52CA6676390D6362");

                entity.Property(e => e.RoutingRuleScheduleTimingKey).ValueGeneratedNever();

                entity.HasOne(d => d.RoutingRuleKeyNavigation)
                    .WithMany(p => p.RoutingRuleScheduleTiming)
                    .HasForeignKey(d => d.RoutingRuleKey)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__RoutingRu__Routi__0E6E26BF");

                entity.HasMany(d => d.ScheduleTimingKeyNavigation)
                    .WithOne(p => p.RoutingRuleScheduleTiming)
                    .HasForeignKey(d => d.ScheduleTimingKey)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__RoutingRu__Sched__0D7A0286");
            });

            modelBuilder.Entity<RoutingRuleTranPriority>(entity =>
            {
                entity.HasKey(e => e.RoutingRuleTranPriorityKey)
                    .HasName("PK__RoutingR__4DA1E626D911327A");

                entity.Property(e => e.RoutingRuleTranPriorityKey).ValueGeneratedNever();

                entity.HasOne(d => d.RoutingRuleKeyNavigation)
                    .WithMany(p => p.RoutingRuleTranPriority)
                    .HasForeignKey(d => d.RoutingRuleKey)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__RoutingRu__Routi__55F4C372");

                entity.HasOne(d => d.TranPriorityKeyNavigation)
                    .WithMany(p => p.RoutingRuleTranPriority)
                    .HasForeignKey(d => d.TranPriorityKey)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__RoutingRu__TranP__55009F39");
            });

            modelBuilder.Entity<SmartSort>(entity =>
            {
                entity.HasKey(e => new { e.TransPriorityKey, e.SmartSortColumnKey })
                    .HasName("pk__smartsor__3ee1053ec47e2d08");

                entity.HasOne(d => d.SmartSortColumnKeyNavigation)
                    .WithMany(p => p.SmartSort)
                    .HasForeignKey(d => d.SmartSortColumnKey)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("fk__smartsort__smart__625a9a57");

                entity.HasOne(d => d.TransPriorityKeyNavigation)
                    .WithMany(p => p.SmartSort)
                    .HasForeignKey(d => d.TransPriorityKey)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("fk__smartsort__trans__6166761e");
            });


            modelBuilder.Entity<SiteConfiguration.API.TransactionPriority.Models.TransactionPriority>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("pk__transact__3fc0e8949c65ee1d");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.ADUFlag).HasColumnName("aduflag");

                entity.Property(e => e.LegendBackColor).HasMaxLength(25);

                entity.Property(e => e.LegendForeColor).HasMaxLength(25);

                entity.Property(e => e.PriorityCode)
                    .IsRequired()
                    .HasMaxLength(25);

                entity.Property(e => e.PriorityName)
                    .IsRequired()
                    .HasMaxLength(50);
            });



        }

     

    }
}

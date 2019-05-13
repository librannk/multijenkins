using BD.Core.ElasticClient.SQL;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoUsageApp.Infrastructure
{
    public class FacilityDBContext : ElasticDbContext
    {

        ///// <summary>
        /// Constructor with db context parameters
        /// </summary>
        /// <param name="options"></param>
        public FacilityDBContext(IServiceProvider serviceProvider,
            DbContextOptions options) : base(serviceProvider, options)
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
        /// Facility entities
        /// </summary>
        public DbSet<Facility> Facilities { get; set; }
        /// <summary>
        /// Transaction Priority Entities
        /// </summary>
       

    }
}

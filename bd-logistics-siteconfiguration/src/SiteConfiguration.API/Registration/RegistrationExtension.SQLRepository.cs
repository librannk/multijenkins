using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SiteConfiguration.API.Infrastructure.DataAccess.SQL.DBContextEntities;
using SiteConfiguration.API.Infrastructure.DataAccess.SQL.DBContextEntities.UnitOfWork;

namespace SiteConfiguration.API.Registration
{
    /// <summary>
    /// To Register all entity for DI for SQL EF repository
    /// </summary>
    public static partial class RegistrationExtension
    {
        /// <summary>
        /// Register all entity for DI.
        /// Register DB Context and pass connection string.
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"> To get the all configured data appsettings.json file </param>
        /// <returns></returns>
        public static IServiceCollection RegisterSqlRepository(this IServiceCollection services, IConfiguration configuration)
        {           
            services.AddEntityFrameworkSqlServer().AddDbContext<ApplicationDbContext>(ServiceLifetime.Scoped, ServiceLifetime.Scoped);

            return services;
        }
    }
}

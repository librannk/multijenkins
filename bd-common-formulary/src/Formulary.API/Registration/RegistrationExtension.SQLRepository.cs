using Formulary.API.Infrastructure.DataAccess.SQL.DBContextEntities.EntityRepositoryImplementation;
using Formulary.API.Infrastructure.DataAccess.SQL.DBContextEntities;
using Formulary.API.Infrastructure.DataAccess.SQL.DBContextEntities.EntityRepositoryContracts;
using Formulary.API.Infrastructure.DataAccess.SQL.DBContextEntities.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace Formulary.API.Registration
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
            var connection = configuration["ConnectionStrings:SQLConnectionString"];
            services.AddEntityFrameworkSqlServer().AddDbContext<ApplicationDBContext>(option =>
            option.UseSqlServer(connection), ServiceLifetime.Scoped, ServiceLifetime.Scoped);
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IFormularyRepository, FormularyRepository>();
            services.AddScoped<IFacilityRepository, FacilityRepository>();
            services.AddScoped<INDCRepository, NDCRepository>();
            services.AddScoped<IFacilityNDCAssocRepository, FacilityNDCAssocRepository>();
            services.AddScoped<IItemRepository, ItemRepository>();
            services.AddScoped<IMedicationItemRepository, MedicationItemRepository>();

            return services;
        }
    }
}

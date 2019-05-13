using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Formulary.API.BusinessLayer.Concrete;
using Formulary.API.BusinessLayer.Contract;
using Formulary.API.Infrastructure.DataAccess.SQL.DBContextEntities.EntityRepositoryContracts;
using Formulary.API.Infrastructure.DataAccess.SQL.DBContextEntities.EntityRepositoryImplementation;
using Microsoft.Extensions.DependencyInjection;

namespace Formulary.API.Registration
{
    /// <summary> Partial extension class for registration. </summary>
    /// NOTE: Please refer all RegistrationExtension classes to get the broader picture
    public static partial class RegistrationExtension
    {
        #region IServiceCollection Extension Methods
        /// <summary> Register all the handlers </summary>
        /// <param name="services"> Instance of IServiceCollection </param>
        /// <returns> services (Instance of IServiceCollection) </returns>
        public static IServiceCollection RegisterManager(this IServiceCollection services)
        {
           services.AddScoped<IFormularyManager, FormularyManager>();
           services.AddScoped<IMedicationItemRepository, MedicationItemRepository>();
           services.AddScoped<IProductIdentificationRepository, ProductIdentificationRepository>();
           services.AddScoped<ISystemItemSetUpManager, SystemItemSetUpManager>();
           services.AddScoped<IItemSetupManager, ItemSetupManager>();
            return services;
        }
        #endregion
    }
}


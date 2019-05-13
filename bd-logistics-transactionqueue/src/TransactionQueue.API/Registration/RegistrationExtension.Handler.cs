using TransactionQueue.API.Application.BussinessLayer.Abstraction;
using TransactionQueue.API.Application.BussinessLayer.Concrete;
using Microsoft.Extensions.DependencyInjection;
using TransactionQueue.Ingestion.BusinessLayer.Concrete;
using TransactionQueue.Ingestion.BusinessLayer.Abstraction;
using TransactionQueue.ExternalDependencies.BusinessLayer.Abstraction;
using TransactionQueue.ExternalDependencies.BusinessLayer.Concrete;

namespace TransactionQueue.API.Registration
{
    /// <summary> Partial extension class for registration. </summary>
    /// NOTE: Please refer all RegistrationExtension classes to get the broader picture
    public static partial class RegistrationExtension
    {
        #region IServiceCollection Extension Methods
        /// <summary> Register all the handlers </summary>
        /// <param name="services"> Instance of IServiceCollection </param>
        /// <returns> services (Instance of IServiceCollection) </returns>
        public static IServiceCollection RegisterHandler(this IServiceCollection services)
        {
            services.AddScoped<ITransactionQueueManager, TransactionQueueManager>();
            services.AddScoped<ITransactionManager, TransactionManager>();
            services.AddScoped<IAduTransactionManager, AduTransactionManager>();
            services.AddScoped<IFacilityManager, FacilityManager>();
            services.AddScoped<IFormularyManager, FormularyManager>();
            services.AddScoped<ITransactionPriorityManager, TransactionPriorityManager>();
            services.AddScoped<IDestinationManager, DestinationManager>();
            return services;
        }
        #endregion
    }
}

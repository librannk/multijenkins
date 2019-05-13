using Autofac;
using System.Reflection;
using BD.Core.EventBus.Abstractions;
using Logistics.Services.DeviceCommunication.API.IntegrationEvents.EventHandling;

namespace Logistics.Services.DeviceCommunication.API.AutofacModule
{
    /// <summary>
    /// class ApplicationModule
    /// </summary>
    public class ApplicationModule
        : Autofac.Module
    {
        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public ApplicationModule(){ }

        #endregion

        #region Protected Methods

        /// <summary>
        /// Load method
        /// </summary>
        /// <param name="builder"></param>
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(ProcessTransactionQueueEventHandler).GetTypeInfo().Assembly)
                .AsClosedTypesOf(typeof(IEventHandler<>));
        }

        #endregion
    }
}

using Autofac;

using BD.Core.EventBus.Abstractions;
using Formulary.API.BusinessLayer.Concrete;
using Formulary.API.BusinessLayer.Contract;
using Formulary.API.IntegrationEvents.Events;
using System.Reflection;

namespace Formulary.API.AutofacModule
{
    /// <summary>
    /// ApplicationModule class is used to register dependencies 
    /// </summary>
    public class ApplicationModule: Autofac.Module
    {
        /// <summary>
        /// Register dependencies
        /// </summary>
        /// <param name="builder"></param>
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(FormularyUpdatedIntegrationEvent).GetTypeInfo().Assembly).AsClosedTypesOf(typeof(IEventHandler<>));
            builder.RegisterAssemblyTypes(typeof(FormularyFacilityUpdatedIntegrationEvent).GetTypeInfo().Assembly).AsClosedTypesOf(typeof(IEventHandler<>));
            builder.RegisterAssemblyTypes(typeof(NDCUpdatedIntegrationEvent).GetTypeInfo().Assembly).AsClosedTypesOf(typeof(IEventHandler<>));
            //builder.RegisterType<FormularyManager>().As<IFormularyManager>();
        }
    }
}

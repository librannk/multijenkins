using Autofac;
using System.Reflection;
using CCEProxy.API.Infrastructure.DataAccess.Repository;
using CCEProxy.API.Infrastructure.DataAccess.Repository.Interfaces;
using CCEProxy.API.IntegrationEvents.EventHandling;
using CCEProxy.API.BusinessLayer.Contracts;
using CCEProxy.Repository.Contracts;
using CCEProxy.API.BusinessLayer.Concrete;
using CCEProxy.API.DataLayer.Concrete;
using BD.Core.EventBus.Abstractions;

namespace CCEProxy.API.AutofacModule
{
    /// <summary>
    /// This class is used for registering event handler dependencies
    /// </summary>
    public class ApplicationModule : Autofac.Module
    {
        #region Protected members
        /// <summary>
        /// Register dependencies
        /// </summary>
        /// <param name="builder"></param>
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(TransactionPriorityIntegrationEventHandler).GetTypeInfo().Assembly)
                .AsClosedTypesOf(typeof(IEventHandler<>));
            builder.RegisterAssemblyTypes(typeof(FacilityIntegrationEventHandler).GetTypeInfo().Assembly)
                .AsClosedTypesOf(typeof(IEventHandler<>));

            builder.RegisterType<TransactionPriorityRepository>().As<ITransactionPriorityRepository>();
            builder.RegisterType<FacilityRepository>().As<IFacilityRepository>();
            builder.RegisterType<IncomingRequestRepository>().As<IIncomingRequestRepository>();
            builder.RegisterType<RequestManager>().As<IRequestManager>();
            builder.RegisterType<RequestRepository>().As<IRequestRepository>();
            builder.RegisterType<FacilityManager>().As<IFacilityManager>();
            builder.RegisterType<TransactionPriorityManager>().As<ITransactionPriorityManager>();
            builder.RegisterType<FacilityIntegrationEventHandler>();
            builder.RegisterType<TransactionPriorityIntegrationEventHandler>();

        }
        #endregion
    }
}

using Autofac;
using BD.Core.EventBus.Abstractions;
using System.Reflection;
using StorageSpace.API.BusinessLayer.Concrete;
using StorageSpace.API.BusinessLayer.Contracts;
using StorageSpace.API.Infrastructure.Repository.Concrete;
using StorageSpace.API.Infrastructure.Repository.Contracts;
using StorageSpace.API.IntegrationEvents.EventHandling;

namespace StorageSpace.API.AutofacModule
{
    /// <summary> ApplicationModule inherits Autofac.Module class </summary>
    public class ApplicationModule : Autofac.Module
    {
        #region Constructors
        /// <summary> ApplicationModule constructor </summary>
        public ApplicationModule()
        {
        }
        #endregion

        #region Protected Methods
        /// <summary> Overrides Load method </summary>
        /// <param name="builder"></param>
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(StorageSpaceRequestEventHandler).GetTypeInfo().Assembly)
                .AsClosedTypesOf(typeof(IEventHandler<>));
            builder.RegisterAssemblyTypes(typeof(StorageSpaceResponseEventHandler).GetTypeInfo().Assembly)
                .AsClosedTypesOf(typeof(IEventHandler<>));

            builder.RegisterType<StorageSpaceManager>().As<IStorageSpaceManager>();
            builder.RegisterType<StorageSpaceDetailMongoRepository>().As<IStorageSpaceDetailMongoRepository>();
            builder.RegisterType<ISAManager>().As<IISAManager>();
            builder.RegisterType<ISADetailRepository>().As<IISADetailRepository>();

        }
        #endregion
    }
}

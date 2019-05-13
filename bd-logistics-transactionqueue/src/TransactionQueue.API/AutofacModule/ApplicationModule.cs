using Autofac;
using BD.Core.EventBus.Abstractions;
using TransactionQueue.API.Infrastructure.Repository;
using TransactionQueue.API.Infrastructure.Repository.Interfaces;
using TransactionQueue.ExternalDependencies.IntegrationEvents.EventHandling;
using System.Reflection;
using TransactionQueue.ManageQueues.Business.Concrete;
using TransactionQueue.ManageQueues.Business.Abstraction;
using TransactionQueue.ManageQueues.Infrastructure.Repository.Concrete;
using TransactionQueue.Ingestion.IntegrationEvents.EventHandling;
using TransactionQueue.Ingestion.Infrastructure.Infrastructure.Repository;
using TransactionQueue.ExternalDependencies.Infrastructure.Repository;
using TransactionQueue.ExternalDependencies.BusinessLayer.Repository;
using TransactionQueue.Ingestion.BusinessLayer.Repository;

namespace TransactionQueue.API.AutofacModule
{
    /// <summary>
    /// This class is used for registering event hanlder dependecies
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
            builder.RegisterAssemblyTypes(typeof(TransactionQueueAddedIntegrationEventHandler).GetTypeInfo().Assembly)
                .AsClosedTypesOf(typeof(IEventHandler<>));
            builder.RegisterAssemblyTypes(typeof(FormularyLocationIntegrationEventHandler).GetTypeInfo().Assembly)
                .AsClosedTypesOf(typeof(IEventHandler<>));
            builder.RegisterAssemblyTypes(typeof(FacilityIntegrationEventHandler).GetTypeInfo().Assembly)
                .AsClosedTypesOf(typeof(IEventHandler<>));
          

            #region register custom mongo repositories
            builder.RegisterType<TransactionQueueMongoRepository>().As<ITransactionQueueMongoRepository>();
            builder.RegisterType<FacilityMongoRepository>().As<IFacilityRepository>();
            builder.RegisterType<TransactionPriorityMongoRepository>().As<ITransactionPriorityRepository>();
            builder.RegisterType<FormularyMongoRepository>().As<IFormularyRepository>();
            builder.RegisterType<LastAduXrefMongoRepository>().As<ILastAduXrefRepository>();
            builder.RegisterType<DestinationMongoRepository>().As<IDestinationRepository>();
            builder.RegisterType<TransactionQueueBussiness>().As<ITransactionQueueBussiness>();
            builder.RegisterType<QueueFilter>().As<IQueueFilter>();
            builder.RegisterType<ColumnSorting>().As<ISortStrategy>();
            builder.RegisterType<SmartSorting>().As<ISortStrategy>();
            builder.RegisterType<PriorityRules>().As<IPriorityRules>();
            builder.RegisterType<TransactionQueueRepository>().As<ManageQueues.Business.Abstraction.ITransactionQueueRepository>();
            builder.RegisterType<Ingestion.Infrastructure.Repository.TransactionQueueMongoRepository>().As<Ingestion.BusinessLayer.Repository.ITransactionQueueRepository>();
            builder.RegisterType<Ingestion.Infrastructure.Repository.TransactionQueueHistoryMongoRepository>().As<ITransactionQueueHistoryRepository>();
            #endregion
        }
        #endregion
    }
}

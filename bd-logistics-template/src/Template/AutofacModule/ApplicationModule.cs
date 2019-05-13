using Autofac;
using BD.Template.API.Infrastructure.Repository.Interfaces;
using BD.Template.API.IntegrationEvents.Events;
using System.Reflection;
using Logistics.Services.Template.API.Infrastructure.Repository;
using BD.Core.EventBus.Abstractions;
using BD.Template.API.IntegrationEvents.Events;
using BD.Template.API.Infrastructure.Repository.Interfaces;
using BD.Template.API.Infrastructure.Repository;

namespace BD.Template.API.AutofacModule
{
    /// <summary>
    /// 
    /// </summary>
    public class ApplicationModule
        : Autofac.Module
    {


        /// <summary>
        /// 
        /// </summary>
        public ApplicationModule()
        {
        }

        /// <summary>
        /// This method is used to register the types using autofac
        /// </summary>
        /// <param name="builder"></param>
        protected override void Load(ContainerBuilder builder)
        {

            builder.RegisterAssemblyTypes(typeof(TemplateAddedIntegrationEvent).GetTypeInfo().Assembly)
                .AsClosedTypesOf(typeof(IEventHandler<>));

            builder.RegisterType<MongoRepository>().As<IMongoRepository>();
            builder.RegisterType<KafkaResponseRepository>().As<IKafkaResponseRepository>();
        }
    }
}

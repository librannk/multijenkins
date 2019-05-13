using Autofac;
using System.Reflection;
using BD.Core.EventBus.Abstractions;

namespace SiteConfiguration.API.AutofacModule
{
    public class ApplicationModule : Autofac.Module
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
            
        }
    }
}

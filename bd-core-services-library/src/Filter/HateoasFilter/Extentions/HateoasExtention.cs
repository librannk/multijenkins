using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;

namespace HateoasFilter.Extentions
{
    public static class HateoasExtention
    {
        #region AddServices in ConfigureServices method of Startup.cs
        /// <summary> Add Hateoas filter in startup</summary>
        /// <param name="services">IServiceCollection</param>
        /// <returns>Same IServiceCollection instance </returns>
        public static IServiceCollection AddHateoasServices(this IServiceCollection services)
        {
            services.AddHateoasService();

            return services;
        }

        /// <summary> Adds Hateoas filter </summary>
        /// <param name="services">IServiceCollection</param>
        /// <returns> Same IServiceCollection</returns>
        public static IServiceCollection AddHateoasService(this IServiceCollection services)
        {
            services.AddScoped<HateoasFilter>();

            return services;
        }
        #endregion

    }
}

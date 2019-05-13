using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using SiteConfiguration.API.Configuration;
using SiteConfiguration.API.RoutingRules.Abstractions;
using SiteConfiguration.API.RoutingRules.Business;
using SiteConfiguration.API.Schedule.Abstractions;
using SiteConfiguration.API.Schedule.Business;
using SiteConfiguration.API.Schedule.Repository;
using SiteConfiguration.API.RoutingRules.Repository;
using SiteConfiguration.API.Infrastructure.DataAccess.SQL.DBContextEntities.UnitOfWork;
using SiteConfiguration.API.TransactionPriority.Abstractions;
using SiteConfiguration.API.TransactionPriority.Repository;
using SiteConfiguration.API.TransactionPriority.RequestResponseModel;
using SiteConfiguration.API.TransactionPriority.ModelValidator;
using SiteConfiguration.API.TransactionPriority.Business;
using FluentValidation;
using SiteConfiguration.API.Printers.Abstractions;
using SiteConfiguration.API.Printers.Repository;
using SiteConfiguration.API.Printers.Business;
using SiteConfiguration.API.FacilityConfiguration.Abstractions;
using SiteConfiguration.API.FacilityConfiguration.Business;
using SiteConfiguration.API.FacilityConfiguration.Repository;

namespace SiteConfiguration.API.Registration
{
    /// <summary> Partial extension class for registration. </summary>
    /// NOTE: Please refer all RegistrationExtension classes to get the broader picture
    public static partial class RegistrationExtension
    {
        // METHODS
        #region IServiceCollection Extension Methods
        /// <summary> Register all Swagger dependencies </summary>
        /// <param name="services"> Instance of IServiceCollection </param>
        /// <returns> services (Instance of IServiceCollection) </returns>
        public static IServiceCollection RegisterDependency(this IServiceCollection services)
        {

            services.AddScoped<IRoutingRuleManager, RoutingRuleManager>();
            services.AddScoped<RoutingRuleManager>();
            services.AddScoped<ISmartSortManager,SmartSortManager>();
            services.AddScoped<IScheduleBusiness, ScheduleBusiness>();
            services.AddScoped<IScheduleRepository, ScheduleRepository>();
            services.AddScoped<IRoutingRuleScheduleTimingRepository, RoutingRuleScheduleTimingRepository>();
            services.AddScoped<IRoutingRuleRepository, RoutingRuleRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<ITransactionPriorityRepository, TransactionPriorityRepository>();
            services.AddScoped<IValidator<TransactionPriorityPost>, TransactionPriorityValidator>();
            services.AddScoped<ISmartSortRepository, SmartSortRepository>();
            services.AddScoped<ITransactionPrioritySmartSortRepository, TransactionPrioritySmartSortRepository>();
            services.AddScoped<ITransactionPriorityManager,TransactionPriorityManager>();
            services.AddScoped<IPrinterBusiness, PrinterBusiness>();
            services.AddScoped<IPrinterRepository, PrinterRepository>();
            services.AddScoped<IPrinterModelRepository, PrinterModelRepository>();
            #region FacilityLogistcsConfigurations
            services.AddScoped<IFacilityLogisticsConfiguration, FacilityLogisticsConfiguration>();
            services.AddScoped<IFacilityLogisticsConfigurationRepository, FacilityLogisticsConfigurationRepository>();
            #endregion



            return services;
        }
        #endregion
    }
}

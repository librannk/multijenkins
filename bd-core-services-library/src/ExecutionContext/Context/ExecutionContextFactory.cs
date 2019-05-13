using System;
using Microsoft.Extensions.DependencyInjection;

namespace BD.Core.Context
{
    /// <summary> Factory for creating execution context </summary>
    public class ExecutionContextFactory : IExecutionContextFactory
    {
        private readonly IExecutionContextAccessor _executionContextAccessor;
        
        public ExecutionContextFactory(IServiceProvider serviceProvider)
        {
            _executionContextAccessor = serviceProvider.GetService<IExecutionContextAccessor>();
        }
        public Context Create(TenantContext TenantKey, FacilityContext Facility,string Locale)
        {
            var executionContext= new Context
            {
                Tenant = TenantKey,
                Facility = Facility,
                Locale = Locale
            };
            if (_executionContextAccessor != null)
            {
                _executionContextAccessor.Current = executionContext;
            }
            return executionContext;
        }
        public void SetContext(Context context)
        {
            if (_executionContextAccessor != null)
            {
                _executionContextAccessor.Current = context;
            }
        }
    }
}
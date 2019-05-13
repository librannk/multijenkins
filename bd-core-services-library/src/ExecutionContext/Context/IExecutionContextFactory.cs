
namespace BD.Core.Context
{
    public interface IExecutionContextFactory
    {
        Context Create(TenantContext Tenant, FacilityContext Facility, string Locale);
        void SetContext(Context context);
    }
}
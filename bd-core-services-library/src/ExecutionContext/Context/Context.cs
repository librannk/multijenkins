using System;
using System.Collections.Generic;

namespace BD.Core.Context
{
    /// <summary> Execution Context </summary>
    [Serializable]
    public class Context
    {
        public TenantContext Tenant;
        public FacilityContext Facility;
        public string Locale;
        public string Version;
        public Dictionary<string,string> Items = new Dictionary<string,string>();
    }
    [Serializable]
    public class FacilityContext
    {
        public FacilityContext(string facilityKey, string facilityCode)
        {
            FacilityKey = facilityKey;
            facilityCode = FacilityCode;
        }
        public string FacilityKey;
        public string FacilityCode;
        public TimeZoneInfo TimeZone;
    }
    [Serializable]
    public class TenantContext
    {
        public TenantContext(string tenantKey)
        {
            TenantKey = tenantKey;
        }
        public string TenantKey;
        public string TenantCode;
    }
}
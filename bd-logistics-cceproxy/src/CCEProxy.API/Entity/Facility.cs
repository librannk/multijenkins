namespace CCEProxy.API.Entity
{
    /// <summary> This model contains detail of Facility</summary>
    public class Facility
    {
        /// <summary>
        /// To hold the Id
        /// </summary>
        public virtual int Id { get; set; }

        /// <summary>
        /// To hold the FacilityCode
        /// </summary>
        public string FacilityCode { get; set; }

    }
}

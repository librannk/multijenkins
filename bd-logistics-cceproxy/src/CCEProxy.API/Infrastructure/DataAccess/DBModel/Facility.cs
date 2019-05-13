
namespace CCEProxy.API.Infrastructure.DataAccess.DBModel
{
    /// <summary>
    /// DB model containing detail of Facility
    /// </summary>
    public class Facility : Mongo.Entities.Entity
    {
        /// <summary>
        /// FacilityCode
        /// </summary>
        public string FacilityCode { get; set; }
        /// <summary>
        /// FacilityId
        /// </summary>
        public int FacilityId { get; set; }
    }
}

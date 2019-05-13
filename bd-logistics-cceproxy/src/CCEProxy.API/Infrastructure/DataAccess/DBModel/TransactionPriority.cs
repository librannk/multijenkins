
namespace CCEProxy.API.Infrastructure.DataAccess.DBModel
{
    /// <summary>
    /// DB model containing detail of TransactionPriority
    /// </summary>
    public class TransactionPriority: Mongo.Entities.Entity
    {
        /// <summary>
        /// PriorityCode
        /// </summary>
        public string PriorityCode { get; set; }
        /// <summary>
        /// IsActive
        /// </summary>
        public bool IsActive { get; set; }
        /// <summary>
        /// FacilityId
        /// </summary>
        public int FacilityId { get; set; }
        /// <summary>
        /// TransactionPriorityId
        /// </summary>
        public int TransactionPriorityId { get; set; }
    }
}

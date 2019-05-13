
namespace CCEProxy.API.Entity
{
    /// <summary> This model contains Transaction Priority of Items</summary>
    public class TransactionPriority
    {
        /// <summary>
        /// To hold the Id field
        /// </summary>
        public virtual int Id { get; set; }

        /// <summary>
        /// To hold the Priority Code
        /// </summary>
        public string PriorityCode { get; set; }
        /// <summary>
        /// To hold the IsActive
        /// </summary>
        public bool IsActive { get; set; }
        /// <summary>
        /// To hold the FacilityId
        /// </summary>
        public int FacilityId { get; set; }

    }
}

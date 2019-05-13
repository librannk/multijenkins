using BD.Core.EventBus.Events;

namespace Facility.API.IntegrationEvents.Events
{
    /// <summary>
    ///FacilityAddedIntegrationEvent: This is Facility type event 
    ///</summary> 
    public class FacilityAddedIntegrationEvent : Event
    {
        #region Properties

        /// <summary>
        /// Facility Identifier
        /// </summary>
        public int FacilityId { get; set; }
        /// <summary>
        /// Facility IsActive
        /// </summary>
        public bool IsActive { get; set; }
        /// <summary>
        /// Facility Is Process Inactive As Exception
        /// </summary>
        public bool ProcessInactiveAsException { get; set; }
        /// <summary>
        /// Facility Code
        /// </summary>
        public string FacilityCode { get; set; }
        /// <summary>
        /// ADUIgnoreStockOut
        /// </summary>
        public bool ADUIgnoreStockOut { get; set; }
        /// <summary>
        /// AduIgnoreCritLow
        /// </summary>
        public bool AduIgnoreCritLow { get; set; }


        #endregion


    }
}

using BD.Core.EventBus.Events;

namespace CCEProxy.API.IntegrationEvents.Events
{
    /// <summary>
    ///FacilityAddedIntegrationEvent: This is Facility type event 
    ///</summary> 
    public class FacilityAddedIntegrationEvent : Event
    {
        /// <summary>
        /// Facility Identifier
        /// </summary>
        public int FacilityId { get; set; }
        /// <summary>
        /// FacilityCode
        /// </summary>
        public string FacilityCode { get; set; }
    }
}

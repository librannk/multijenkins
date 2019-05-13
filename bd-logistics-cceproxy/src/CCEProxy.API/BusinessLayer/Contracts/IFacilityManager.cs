using CCEProxy.API.Entity;
using System.Threading.Tasks;

namespace CCEProxy.API.BusinessLayer.Contracts
{
    /// <summary> This interface is responsible for handling the Facility Queue operation </summary>
    public interface IFacilityManager
    {
        /// <summary> This method is used for filtering the request and save the data into database </summary>
        /// <param name="facilityRequest"> Request</param>
        Task ProcessFacilityRequest(Facility facilityRequest);
    }
}

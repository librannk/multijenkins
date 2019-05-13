using Logistics.Services.DeviceCommunication.API.Application.Models;
using Logistics.Services.DeviceCommunication.API.DeviceInterface.Interfaces;
using Logistics.Services.DeviceCommunication.API.DeviceInterface.Models;
using System.Threading.Tasks;

namespace Logistics.Services.DeviceCommunication.API.Application.Strategy
{
    /// <summary>
    /// Interface to define strategy
    /// </summary>
    public interface ICarouselManager
    {

        /// <summary>
        /// Strategy to create device object
        /// </summary>
        Task<IDeviceResponse> CreateCarousel(TransactionData transactionData);

        /// <summary>
        /// Strategy to perform an action 
        /// </summary>
        Task<IDeviceResponse> MoveCarousel(TransactionData transactionData, Slot slot);

    }
}

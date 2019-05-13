using Logistics.Services.DeviceCommunication.API.Application.Models;
using Logistics.Services.DeviceCommunication.API.DeviceInterface.Interfaces;
using System.Threading.Tasks;

namespace Logistics.Services.DeviceCommunication.API.Application.Interfaces
{
    /// <summary>
    /// ICarouselProcess contract
    /// </summary>
    public interface ICarouselProcess
    {

        /// <summary>
        /// method to process carousel move using data coming from transaction queue service
        /// </summary>
        /// <param name="transactionData"></param>
        Task<IDeviceResponse> MoveCarousel(TransactionData transactionData);
    }
}
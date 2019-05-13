using Logistics.Services.DeviceCommunication.API.Application.Models;
using System.Threading.Tasks;

namespace Logistics.Services.DeviceCommunication.API.DeviceInterface.Interfaces
{
    /// <summary>
    /// Interface to implement the carousal factory class
    /// </summary>
    public interface ICarouselFactory
    {
        #region Methods

        /// <summary>
        /// Instantiate Carousel 
        /// </summary>
        /// <param name="carouselData"></param>
        /// <param name="deviceResponse"></param>
        /// <param name="socket"></param>
        /// <returns></returns>
        Task<ICarousel> GetCarouselType(Device carouselData, IDeviceResponse deviceResponse, IIPSocket socket, string timeOut);

        #endregion
    }
}

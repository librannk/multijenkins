using Logistics.Services.DeviceCommunication.API.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Logistics.Services.DeviceCommunication.API.Application.Interfaces
{
    /// <summary>
    /// Contract for CarouselConnection.
    /// </summary>

    public interface ICarouselConnection
    {
        /// <summary>
        /// Checks whether a carousel is connected or not.
        /// </summary>
        /// <param name="carouselData"></param>
        /// <returns></returns>
        IEnumerable<CarouselData> Check(IEnumerable<CarouselData> carouselData);
    }
}

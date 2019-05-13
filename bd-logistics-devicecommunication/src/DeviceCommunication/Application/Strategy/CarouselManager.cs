using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;
using Logistics.Services.DeviceCommunication.API.Application.Models;
using Logistics.Services.DeviceCommunication.API.DeviceInterface.Interfaces;
using Logistics.Services.DeviceCommunication.API.DeviceInterface.Models;
using Logistics.Services.DeviceCommunicationAPI.Application.Common;
using Logistics.Services.DeviceCommunication.API.DeviceInterface.Carousels;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace Logistics.Services.DeviceCommunication.API.Application.Strategy
{
    /// <summary>
    /// Strategy to create and use carousal
    /// </summary>
    public class CarouselManager : ICarouselManager
    {
        #region Fields

        private readonly ICarouselFactory _carouselFactory = null;
        private readonly ILogger<CarouselManager> _logger;
        private readonly IIPSocket _socket;
        private ICarousel _carousel = null;
        private IDeviceResponse _deviceResponse;
        private readonly IConfiguration _configuration;

        #endregion

        #region Constructor

        /// <summary>
        /// Default Constructor
        /// </summary>
        /// <param name="config"></param>
        /// <param name="logger"></param>
        /// <param name="carouselFactory"></param>
        /// <param name="socket"></param>
        /// <param name="deviceResponse"></param>
        public CarouselManager(IConfiguration config, ILogger<CarouselManager> logger, ICarouselFactory carouselFactory, IIPSocket socket, IDeviceResponse deviceResponse)
        {
            _carouselFactory = carouselFactory;
            _logger = logger;
            _socket = socket;
            _deviceResponse = deviceResponse;
            _configuration = config;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Method to create specific type of carousel instance depending upon the data provided
        /// </summary>
        /// <param name="transactionData"></param>
        public async Task<IDeviceResponse> CreateCarousel(TransactionData transactionData)
        {
            //Timeout value for carousel
            string timeOut = _configuration.GetSection("Settings").GetChildren()
                .Select(item => new KeyValuePair<string, string>(item.Key, item.Value))
                .ToDictionary(x => x.Key, x => x.Value)["CarouselTimeout"];

            Device carouselData = transactionData.Devices.FirstOrDefault(dev => dev.Type.Equals("Carousel"));
            _carousel = await _carouselFactory.GetCarouselType(carouselData, _deviceResponse, _socket, timeOut);

            if (_carousel == null)
            {
                _deviceResponse.Message = Constants.CarouselManager.ControllerTypeNotFound;
                _deviceResponse.HasError = true;
            }

            _logger.LogDebug(Constants.CarouselManager.TransactionQueueCarouselIsCreatedAndCarouselInstantiated);
            return _deviceResponse;
        }

        /// <summary>
        /// Strategy to move carousel
        /// </summary>
        /// <param name="transactionData"></param>
        /// <param name="slot"></param>
        /// <returns></returns>

        public async Task<IDeviceResponse> MoveCarousel(TransactionData transactionData, Slot slot)
        {
            //Carousel specific parameter passing & property re-initialization before calling move method.
            if (_carousel is CarWhiteIpcDualAccess)
            {
                //TODO: Currently mocking the property : isOutsideComputer. The same to be received in event data
                bool isOutsideComputer = true;

                _deviceResponse = await _carousel.Move(slot: slot, transactionData: transactionData, isOutsideComputer: isOutsideComputer);
            }
            else if (_carousel is CarWhiteTb14702300)
            {
                if (transactionData.Devices.FirstOrDefault(dev => dev.Type.Equals("Carousel")).Attribute.IsDisplayAttached)
                {
                    (_carousel as CarWhiteTb14702300).IgnoreSendMessage = true;
                }

                _deviceResponse = await _carousel.Move(slot: slot, transactionData: transactionData);
            }
            else if (_carousel is CarWhiteTB1470 || _carousel is CarWhiteTB1470H)
            {
                _deviceResponse = await _carousel.Move(slot: slot, transactionData: transactionData);
            }

            return _deviceResponse;
        }

        #endregion
    }
}

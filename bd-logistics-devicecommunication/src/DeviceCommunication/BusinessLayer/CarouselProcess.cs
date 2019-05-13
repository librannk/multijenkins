using System.Linq;
using Microsoft.Extensions.Logging;
using Logistics.Services.DeviceCommunication.API.Application.Strategy;
using Logistics.Services.DeviceCommunication.API.Application.Interfaces;
using Logistics.Services.DeviceCommunication.API.Application.Models;
using Logistics.Services.DeviceCommunication.API.DeviceInterface.Interfaces;
using Logistics.Services.DeviceCommunication.API.DeviceInterface.Models;
using Logistics.Services.DeviceCommunicationAPI.Application.Common;
using System.Threading.Tasks;

namespace Logistics.Services.DeviceCommunication.API.BusinessLayer
{
    /// <summary>
    /// All Carousel processing implementation 
    /// </summary>
    public class CarouselProcess : ICarouselProcess
    {
        #region Fields

        /// <summary>
        /// Declare CarouselProcess type logger
        /// </summary>
        private readonly ILogger<CarouselProcess> _logger;

        /// <summary>
        /// Declare deviceResponse field
        /// </summary>
        private IDeviceResponse _deviceResponse;

        /// <summary>
        /// Carousel manager instance
        /// </summary>
        private readonly ICarouselManager _carouselManager;

        /// <summary>
        /// Declare utility type 
        /// </summary>
        private readonly IUtility _utility;

        #endregion

        #region Constructor

        /// <summary>
        ///  Constructor to initialize logger, deviceResponse and carouselManager
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="deviceResponse"></param>
        /// <param name="carouselManager"></param>
        /// <param name="utility"></param>
        public CarouselProcess(ILogger<CarouselProcess> logger, IDeviceResponse deviceResponse,
                                                    ICarouselManager carouselManager, IUtility utility)
        {
            _logger = logger;
            _deviceResponse = deviceResponse;
            _carouselManager = carouselManager;
            _utility = utility;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Carousel move implementation
        /// </summary>
        /// <param name="transactionData"></param>
        public async Task<IDeviceResponse> MoveCarousel(TransactionData transactionData)
        {
            _deviceResponse.HasError = false;
            _deviceResponse.Message = string.Empty;
            _deviceResponse.CurrentCarrier = 0;

            try
            {
                // Slot Data
                Slot slot = await _utility.BuildStorageSpaceItem(transactionData.Devices.FirstOrDefault(dev => dev.Type.Equals("Carousel"))?.StorageSpaces);
                _logger.LogInformation(Constants.CarousalProcess.SlotObjectCreated);

                // Carousel creation
                _deviceResponse = await _carouselManager.CreateCarousel(transactionData);
                if (!_deviceResponse.HasError)
                {
                    _logger.LogInformation(Constants.CarousalProcess.CarouselCreated);
                }
                else
                {
                    _logger.LogInformation(_deviceResponse.Message);
                    return _deviceResponse;
                }

                // Moving carousel
                _deviceResponse = await _carouselManager.MoveCarousel(transactionData: transactionData, slot: slot);
                if (!_deviceResponse.HasError)
                {
                    _logger.LogInformation(Constants.CarousalProcess.CarouselMoved);
                }
                else
                {
                    _logger.LogInformation(_deviceResponse.Message);
                    return _deviceResponse;
                }
            }
            catch (System.Net.Sockets.SocketException ex)
            {
                _logger.LogError(ex, ex.Message);

                //Initializing the response object
                _deviceResponse.HasError = true;
                _deviceResponse.ErrorCode = ex.ErrorCode;
                _deviceResponse.Message = ex.Message;
            }

            return _deviceResponse;
        }

        #endregion

    }
}

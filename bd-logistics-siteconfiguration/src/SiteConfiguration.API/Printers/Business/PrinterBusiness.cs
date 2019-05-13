using AutoMapper;
using SiteConfiguration.API.Common.Constants;
using SiteConfiguration.API.Infrastructure.DataAccess.SQL.DBContextEntities.UnitOfWork;
using SiteConfiguration.API.Printers.Abstractions;
using SiteConfiguration.API.Printers.Exceptions;
using SiteConfiguration.API.Printers.Models.BuisnessContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SiteConfiguration.API.Printers.Business
{

    /// <summary>
    /// Printer business logic layer.
    /// </summary>
    public class PrinterBusiness : IPrinterBusiness
    {
        private readonly IPrinterRepository _printerRepository;
        private readonly IPrinterModelRepository _printerModelRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;


        /// <summary>
        /// Creates instance of <see cref="PrinterBusiness"/>.
        /// </summary>
        /// <param name="printerRepository"></param>
        /// <param name="printerModelRepository"></param>
        /// <param name="mapper"></param>
        public PrinterBusiness(IPrinterRepository printerRepository, IPrinterModelRepository printerModelRepository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _printerRepository = printerRepository;
            _printerModelRepository = printerModelRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Get list of printer from database.
        /// </summary>
        /// <param name="facilityKey"></param>
        public async Task<IEnumerable<PrinterResponse>> GetPrintersByFacility(Guid facilityKey)
        {
            var printers = await _printerRepository.GetPrintersByFacility(facilityKey);
            return _mapper.Map<IEnumerable<PrinterResponse>>(printers);
        }

        /// <summary>
        /// Get list of printerModel from database.
        /// </summary>
        public async Task<IEnumerable<PrinterModel>> GetPrinterModels()
        {
            var printerModels = await _printerModelRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<PrinterModel>>(printerModels.OrderBy(pm => pm.DescriptionText));
        }

        /// <summary>
        /// Get printer from database.
        /// </summary>
        /// <param name="printerKey"></param>
        public async Task<PrinterResponse> GetPrinterByKey(Guid printerKey)
        {
            var printer = await _printerRepository.GetAsync(printerKey);
            return _mapper.Map<PrinterResponse>(printer);
        }

        /// <summary>
        /// Adds a printer
        /// </summary>
        /// <param name="printerRequest"></param>
        /// <param name="facilityKey"></param>
        /// <returns></returns>
        public async Task AddPrinter(PrinterRequest printerRequest, Guid facilityKey)
        {
            var printerModel = await _printerModelRepository.GetAsync(printerRequest.PrinterModelKey);

            if (printerModel == null)
            {
                throw new InvalidPrinterException(Resource.ResourceManager.GetString($"E{ErrorCode.InvalidInput}"), ErrorCode.InvalidInput);
            }

            var printer = _mapper.Map<Models.Data.Printer>(printerRequest);
            var Id = Guid.NewGuid();
            printer.PrinterKey = Id;
            printer.FacilityKey = facilityKey;
            await _printerRepository.AddAsync(printer);
            _unitOfWork.CommitChanges();
        }

        /// <summary>
        /// update printer database
        /// </summary>
        /// <param name="key"></param>
        /// <param name="facilityKey"></param>
        /// <param name="printer"></param>
        /// <returns></returns>
        public async Task UpdatePrinter(Guid key, Guid facilityKey, PrinterRequest printer)
        {
            var printerModel = await _printerModelRepository.GetAsync(printer.PrinterModelKey);

            if (printerModel == null)
            {
                throw new InvalidPrinterException(Resource.ResourceManager.GetString($"E{ErrorCode.InvalidInput}"), ErrorCode.InvalidInput);
            }

            var dataModel =
                await _printerRepository.GetAsync(key);

            if (dataModel == null)
            {
                throw new InvalidPrinterException(Resource.ResourceManager.GetString($"E{ErrorCode.ResourceNotFound}"), ErrorCode.ResourceNotFound);

            }

            _mapper.Map(printer, dataModel);

            _printerRepository.Update(dataModel);
            _unitOfWork.CommitChanges();
        }
    }
}

using AutoMapper;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Threading.Tasks;
using TransactionQueue.ExternalDependencies.BusinessLayer.Abstraction;
using TransactionQueue.ExternalDependencies.BusinessLayer.Models;
using TransactionQueue.ExternalDependencies.BusinessLayer.Repository;
using TransactionQueue.ExternalDependencies.Common.Constants;

namespace TransactionQueue.ExternalDependencies.BusinessLayer.Concrete
{
    /// <summary>
    /// This class is responsible for handling formulary related operations.
    /// </summary>
    public class FormularyManager : IFormularyManager
    {
        #region Private Fields
        private readonly IFormularyRepository _formularyRepository;
        private readonly ILogger<FormularyManager> _logger;
        private readonly IMapper _mapper;
        #endregion

        #region Constructors
        /// <summary>
        /// Intializes the private fields.
        /// </summary>
        /// <param name="formularyRepository">facilityDal</param>
        /// <param name="logger">logger</param>
        public FormularyManager(IFormularyRepository formularyRepository,
            ILogger<FormularyManager> logger,
            IMapper mapper)
        {
            _formularyRepository = formularyRepository;
            _logger = logger;
            _mapper = mapper;
        }
        #endregion

        #region Public methods

        /// <summary>
        /// This method is used to store a formulary in DB.
        /// </summary>
        /// <param name="formulary">formulary to be inserted/updated.</param>
        public async Task<bool> ProcessFormularyRequest(Formulary formulary)
        {
            _logger.LogInformation(string.Format(CommonConstants.LoggingMessage.DataReceivedFromFormulary, JsonConvert.SerializeObject(formulary)));
            var result = await _formularyRepository.GetFormularyById(formulary.FormularyId);

            if (result == null)
            {
                return await _formularyRepository.InsertFormulary(formulary);
            }
            else
            {
                return await _formularyRepository.UpdateFormulary(formulary);
            }
        }

        /// <summary>
        /// Get Formulary record by ItemId.
        /// </summary>
        /// <param name="itemId">ItemId</param>
        /// <returns></returns>
        public async Task<Formulary> GetFormularyByItemId(int itemId)
        {
            var result = await _formularyRepository.GetFormularyByItemId(itemId);
            if (result != null)
            {
                return _mapper.Map<Formulary>(result);
            }
            return null;
        }

        /// <summary>
        /// This method is used to delete a formulary in DB.
        /// </summary>
        /// <param name="formularyId">formularyId</param>
        public async Task<bool> DeleteFormulary(int formularyId)
        {
            _logger.LogInformation(string.Format(CommonConstants.LoggingMessage.StartDeletingFormulary, formularyId));

            if (await _formularyRepository.DeleteFormulary(formularyId))
            {
                _logger.LogInformation(string.Format(CommonConstants.LoggingMessage.FormularyDeleted, formularyId));
                return true;
            };
            return false;
        }

        /// <summary>
        /// This method is used to store a formulary facility in DB.
        /// </summary>
        /// <param name="formulary">formulary facility to be updated.</param>
        public async Task<bool> UpdateFormularyFacility(Formulary formulary)
        {
            _logger.LogInformation(string.Format(CommonConstants.LoggingMessage.DataReceivedFromFormularyFacility, JsonConvert.SerializeObject(formulary)));
            return await _formularyRepository.UpdateFormularyFacility(formulary);
        }

        #endregion
    }
}

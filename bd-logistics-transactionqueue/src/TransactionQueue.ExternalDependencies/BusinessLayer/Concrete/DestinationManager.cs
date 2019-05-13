using AutoMapper;
using System.Threading.Tasks;
using TransactionQueue.ExternalDependencies.BusinessLayer.Abstraction;
using TransactionQueue.ExternalDependencies.BusinessLayer.Models;
using TransactionQueue.ExternalDependencies.BusinessLayer.Repository;

namespace TransactionQueue.ExternalDependencies.BusinessLayer.Concrete
{
    /// <summary> This class is responsible for handling the Facility data operations </summary>
    public class DestinationManager : IDestinationManager
    {
        #region Private Fields
        private readonly IDestinationRepository _mongoRepository;
        private readonly IMapper _mapper;
        #endregion

        #region Constructors
        /// <summary> Initialize the private fields </summary>
        public DestinationManager(IDestinationRepository mongoRepository, IMapper mapper)
        {
            _mongoRepository = mongoRepository;
            _mapper = mapper;
        }

        #endregion

        #region Data Operations

        /// <summary>
        /// Get Destination record from DB based on DestinationCode.
        /// </summary>
        /// <param name="destinationCode">DestinationCode</param>
        /// <returns></returns>
        public async Task<Destination> GetDestinationByCode(string destinationCode)
        {
            var result = await _mongoRepository.GetDestinationByCode(destinationCode);
            if (result != null)
            {
                return _mapper.Map<Destination>(result);
            }

            return null;
        }

        #endregion
    }
}

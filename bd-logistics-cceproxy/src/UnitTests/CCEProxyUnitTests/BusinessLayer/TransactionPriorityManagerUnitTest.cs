using CCEProxy.API.BusinessLayer.Concrete;
using CCEProxy.API.Entity;
using CCEProxy.Repository.Contracts;
using Microsoft.Extensions.Logging;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace CCEProxyUnitTests.BusinessLayer
{
    /// <summary>
    ///  This class contains unit tests for FacilityManager
    /// </summary>
    public class TransactionPriorityManagerUnitTest
    {
        #region PrivateFields
        private readonly Mock<IRequestRepository> _requestRepository;
        private readonly Mock<ILogger<TransactionPriorityManager>> _logger;
        private readonly TransactionPriorityManager _transactionPriorityManager;
        private readonly TransactionPriority _transactionPriorityRequest;
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes the private fields
        /// </summary>

        public TransactionPriorityManagerUnitTest()
        {
            _requestRepository = new Mock<IRequestRepository>();
            _logger = new Mock<ILogger<TransactionPriorityManager>>();
            _transactionPriorityRequest = new TransactionPriority
            {
                PriorityCode = "PATIENTPICK",
                FacilityId =  1,
                IsActive = true
            };

            _transactionPriorityManager = new TransactionPriorityManager(_requestRepository.Object, _logger.Object);

        }

        #endregion

        #region Test case for InsertTransactionPriorityRequestUnitTest
        [Fact]
        public async Task InsertTransactionPriorityRequest__ShouldInsertTransactionPriority()
        {
            //Act
            await _transactionPriorityManager.ProcessTransactionPriorityRequest(_transactionPriorityRequest);

            //Arrange
            _requestRepository.Verify(x => x.AddTransactionPriorityRequest(_transactionPriorityRequest));
        }
        #endregion
    }
}

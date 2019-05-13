using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Moq;
using TransactionQueue.ManageQueues.Business.Abstraction;
using TransactionQueue.ManageQueues.Business.Concrete;
using TransactionQueue.ManageQueues.Business.Models;
using Xunit;
using TransactionQueue.ManageQueues.Business.Models;

namespace TransactionQueue.UnitTest.BussinessLayer
{

    public class TransactionQueueBussinessPickNow
    {
        #region Fields
        private readonly Mock<ITransactionQueueRepository> _transactionQueueRepository;
        private readonly TransactionQueueBussiness _transactionQueueBussiness;
        #endregion

        #region Constructor
        public TransactionQueueBussinessPickNow()
        {
            _transactionQueueRepository = new Mock<ITransactionQueueRepository>();
            _transactionQueueBussiness = new TransactionQueueBussiness(_transactionQueueRepository.Object);
        }

        #endregion

        #region Test Cases

        [Fact]
        public async Task PickNow_NoIsaFound_Test()
        {
            //Arranage
            PickNow objPickNow = new PickNow();
            Dictionary<string, string> headers = new Dictionary<string, string>();
            List<int?> activeIsas = null;
            _transactionQueueRepository.Setup(x => x.GetActiveISA(It.IsAny<int>())).ReturnsAsync(activeIsas);

            //Act
            BusinessResponse objBusinessResponse= await _transactionQueueBussiness.PickNow("transactionKeyToActivate", 1, objPickNow, headers);

            //Assert
            Assert.Equal(false, objBusinessResponse.IsSuccess);
        }

        [Fact]
        public async Task PickNow_TransactionToActivateNotFound_Test()
        {
            //Arranage
            PickNow objPickNow = new PickNow();
            Dictionary<string, string> headers = new Dictionary<string, string>();
            List<int?> activeIsas = new List<int?>() {1};
            _transactionQueueRepository.Setup(x => x.GetActiveISA(It.IsAny<int>())).ReturnsAsync(activeIsas);

            List<ManageQueues.Business.Models.TransactionQueue> listTransactionQueue = null;


            _transactionQueueRepository.Setup(x => x.GetPendingTransactions(It.IsAny<List<int?>>())).ReturnsAsync(listTransactionQueue);

            //Act
            BusinessResponse objBusinessResponse = await _transactionQueueBussiness.PickNow("transactionKeyToActivate", 1, objPickNow, headers);

            //Assert
            Assert.Equal(false, objBusinessResponse.IsSuccess);
        }


        [Fact]
        public async Task PickNow_TransactionToActivateIsOnHold_Test()
        {
            //Arranage
            PickNow objPickNow = new PickNow() { };
            Dictionary<string, string> headers = new Dictionary<string, string>();
            List<int?> activeIsas = new List<int?>() { 1 };
            _transactionQueueRepository.Setup(x => x.GetActiveISA(It.IsAny<int>())).ReturnsAsync(activeIsas);

            List<ManageQueues.Business.Models.TransactionQueue> listTransactionQueue = new List<ManageQueues.Business.Models.TransactionQueue>() { new ManageQueues.Business.Models.TransactionQueue() { Status = "Hold" } };


            _transactionQueueRepository.Setup(x => x.GetPendingTransactions(It.IsAny<List<int?>>())).ReturnsAsync(listTransactionQueue);

            //Act
            BusinessResponse objBusinessResponse = await _transactionQueueBussiness.PickNow("transactionKeyToActivate", 1, objPickNow, headers);

            //Assert
            Assert.Equal(false, objBusinessResponse.IsSuccess);
        }

        [Fact]
        public async Task PickNow_TransactionIsNotActivated_Test()
        {
            //Arranage
            PickNow objPickNow = new PickNow() {  };
            Dictionary<string, string> headers = new Dictionary<string, string>();
            List<int?> activeIsas = new List<int?>() { 1 };
            _transactionQueueRepository.Setup(x => x.GetActiveISA(It.IsAny<int>())).ReturnsAsync(activeIsas);

            List<ManageQueues.Business.Models.TransactionQueue> listTransactionQueue = new List<ManageQueues.Business.Models.TransactionQueue>() { new ManageQueues.Business.Models.TransactionQueue() { Status = "Hold" } };


            _transactionQueueRepository.Setup(x => x.GetPendingTransactions(It.IsAny<List<int?>>())).ReturnsAsync(listTransactionQueue);
            long updateCount = 0;

            _transactionQueueRepository.Setup(x => x.UpdateTransactionQueueStatus(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<List<int?>>())).ReturnsAsync(updateCount);

            //Act
            BusinessResponse objBusinessResponse = await _transactionQueueBussiness.PickNow("transactionKeyToActivate", 1, objPickNow, headers);

            //Assert
            Assert.Equal(false, objBusinessResponse.IsSuccess);
        }



        [Fact]
        public async Task PickNow_TransactionIsActivated_Test()
        {
            //Arranage
            PickNow objPickNow = new PickNow() {};
            Dictionary<string, string> headers = new Dictionary<string, string>();
            List<int?> activeIsas = new List<int?>() { 1 };
            _transactionQueueRepository.Setup(x => x.GetActiveISA(It.IsAny<int>())).ReturnsAsync(activeIsas);

            List<ManageQueues.Business.Models.TransactionQueue> listTransactionQueue = new List<ManageQueues.Business.Models.TransactionQueue>() { new ManageQueues.Business.Models.TransactionQueue() { Status = "Hold" } };


            _transactionQueueRepository.Setup(x => x.GetPendingTransactions(It.IsAny<List<int?>>())).ReturnsAsync(listTransactionQueue);
            long updateCount = 1;

            _transactionQueueRepository.Setup(x => x.UpdateTransactionQueueStatus(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<List<int?>>())).ReturnsAsync(updateCount);

            //Act
            BusinessResponse objBusinessResponse = await _transactionQueueBussiness.PickNow("transactionKeyToActivate", 1, objPickNow, headers);

            //Assert
            Assert.Equal(true, objBusinessResponse.IsSuccess);
        }
        #endregion
    }
}

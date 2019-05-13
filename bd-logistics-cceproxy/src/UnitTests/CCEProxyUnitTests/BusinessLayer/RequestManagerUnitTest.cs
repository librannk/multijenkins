using Microsoft.Extensions.Logging;
using Moq;
using System.Collections.Generic;
using CCEProxy.API.Entity;
using CCEProxy.Repository.Contracts;
using CCEProxy.API.BusinessLayer.Concrete;
using Xunit;
using static CCEProxy.API.Common.Constants.Constants;
using Microsoft.Extensions.Options;
using CCEProxy.API.Configuration;
using System.Threading.Tasks;
using BD.Core.EventBus.Abstractions;

namespace CCEProxyUnitTests.BusinessLayer
{
    /// <summary>
    ///  This class contains unit tests for RequestManager
    /// </summary>
    public class RequestManagerUnitTest
    {
        #region PrivateFields
        private readonly Mock<IRequestRepository> _requestRepository;
        private readonly Mock<ILogger<RequestManager>> _logger;
        private readonly Mock<IEventBus> _eventBus;
        private readonly IncomingRequest _incomingRequest;
        private readonly Mock<IOptions<Configuration>> _options;
        private readonly RequestManager _requestManager;
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes the private fields
        /// </summary>
        
        public RequestManagerUnitTest()
        {
            _requestRepository = new Mock<IRequestRepository>();
            _logger = new Mock<ILogger<RequestManager>>();
            _eventBus = new Mock<IEventBus>();

            _incomingRequest = new IncomingRequest {
                RequestId = "23",
                Facility = new IncomingFacility()
                {
                    FacilityCode = "23"
                },
                Patient = new Patient
                {
                    Mrn  = "valid",
                    Lastname = "sperber",
                    Firstname = "doug",
                    MiddleName = "bill",
                    Suffix = "test",
                    Gender = "male",
                    DateOfBirth = "8 May 2019",
                    AccountNumber = "1234",
                    Weight = "34",
                    Height = "6",
                    ContactNo = "789643",
                    Room = "67",
                    Bed = "great",
                    VisitId = "56",
                    Dept = "fine",
                    PrescriptionNo = "date",
                    DeliverToLocation = "noida"
                },
                ADM = new ADM
                {
                  AdmTransId =  "1",
                  Drawer = "side",
                  Pocket = "left",
                  StationName = "adutest",
                  Subdrawer = "test"
                },
                UserDef = new UserDef
                {
                    UsrDef1 = "test1",
                    UsrDef2 = "test2",
                    UsrDef3 = "test3",
                    UsrDef4 = "test4",
                    UsrDef5 = "test5"
                },
                Order = new Order
                {
                    CopeOrderNo = "1",
                    IsStatOrder = true,
                    OrderControlId = "23",
                    OrderNo = "23",
                    OrderingDrInstructions = "test",
                    OrderingDueTime = "datetime",
                    OrderingPriority = "test"
                       
                },
                Priority = "PATIENTPICK",
                Status = "RECEIVED",
                StatusMessage = "Validation Successful",
                Items = new List<Item>
                {
                    new Item()
                    {
                        ComponentAmount = "1",
                        ComponentStrength = "300",
                        ComponentStrengthUnits = "MG",
                        ComponentType = "M",
                        Concentration = "void",
                        DispenseAmount = "23",
                        DispenseUnits = "5",
                        ItemId = "48152",
                        ItemName = "Crocin",
                        OrderAmount = "45",
                        PharmacySpecialDispInstructions = "NA",
                        Strength = "34",
                        SupplementaryCode = "34",
                        TotalDose = "4",
                        Volume = "67"
                    }
                }

            };

            _options = new Mock<IOptions<Configuration>>();
            _options.SetupGet(x => x.Value).Returns(new Configuration
            {
                KafkaCCEProxyTopic = "xyz",
            });
            _requestManager = new RequestManager(_options.Object , _eventBus.Object ,_requestRepository.Object, _logger.Object);
           
        }

        #endregion

        #region Test case for InsertIncomingRequestUnitTest
        [Theory]
        [InlineData("5c8a01a5efe48745ac3c98c6")]
        public async Task InsertIncomingRequest_IncomingRequestId_ShouldReturnSameID(string mock)
        {
            //Arrange
            string mockId = mock;
            _requestRepository.Setup(x => x.AddIncomingRequest(_incomingRequest)).Returns(Task.FromResult(mockId));

            //Act
            string incomingRequestId = await _requestManager.InsertIncomingRequest(_incomingRequest);

            //Assert
            Assert.Equal(mockId, incomingRequestId);
        }

        #endregion

        #region Test case for ProcessIncomingRequestUnitTest
        [Theory]
        [InlineData("23")]
        public async Task ProcessIncomingRequest_GettingAllResponses_ShouldReturnEmptyString(string facilityCode)
        {
            //Arrange
            var facility = new Facility() {
                Id = 1,
                FacilityCode = facilityCode
            };

            var transactionPriority = new Mock<TransactionPriority>();
            transactionPriority.SetupGet(x => x.Id).Returns(1);

            _requestRepository.Setup(x => x.GetFacility(It.IsAny<string>())).Returns(Task.FromResult(facility));
            _requestRepository.Setup(x => x.GetTransactionPriority(It.IsAny<int>(), It.IsAny<string>())).Returns(Task.FromResult(transactionPriority.Object));

            _incomingRequest.RequestId = facilityCode;
            var items = new Mock<Item>();
            List<Item> itemDetails = new List<Item>
            {
                items.Object
            };

            _incomingRequest.Items = itemDetails;
            Dictionary<string, string> fakeHeader = new Dictionary<string, string>();

            //Act
            var aggregatorData = await _requestManager.ProcessIncomingRequest(_incomingRequest, "1", fakeHeader);

            //Assert
            Assert.Empty(aggregatorData);
        }

        #endregion


        #region Testcases for ProcessIncomingRequestUnitTestReturningNull

        [Fact]
        public async Task ProcessIncomingRequestUnitTest_NullFacility_ShouldReturnFacilityInvalid()
        {
            //Arrange
            Dictionary<string, string> fakeHeader = new Dictionary<string, string>();

            //Act
            var aggregateData = await _requestManager.ProcessIncomingRequest(_incomingRequest, "1", fakeHeader);

            //Assert
            Assert.Equal(LoggingMessage.FacilityInvalid, aggregateData);
        }

        [Theory]
        [InlineData(1,"23")]
        public async Task ProcessIncomingRequestUnitTes_NullTransactionPriority_ShouldReturnTransactionPriorityInvalid(int facilityId, string facilityCode)
        {
            //Arrange
            var facility = new Facility()
            {
                Id = facilityId,
                FacilityCode = facilityCode,
            };
            _requestRepository.Setup(x => x.GetFacility(It.IsAny<string>())).Returns(Task.FromResult(facility));
            Dictionary<string, string> fakeHeader = new Dictionary<string, string>();

            //Act
            var aggregatorData = await _requestManager.ProcessIncomingRequest(_incomingRequest, "1", fakeHeader);

            //Assert
            Assert.Equal(LoggingMessage.PriorityInvalid,aggregatorData);
        }

        #endregion

    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BD.Core.Context;
using Microsoft.Extensions.Logging;
using Moq;
using SiteConfiguration.API.Infrastructure.DataAccess.SQL.DBContextEntities.UnitOfWork;
using SiteConfiguration.API.TransactionPriority.Abstractions;
using SiteConfiguration.API.TransactionPriority.Business;
using SiteConfiguration.API.TransactionPriority.RequestResponseModel;
using SiteConfiguration.API.TransactionPriority.Models;
using Xunit;
using System.Linq;
using BD.Core.EventBus.Abstractions;
using Microsoft.Extensions.Options;
using SiteConfiguration.API.Configuration;
using AutoMapper;

namespace Test.TransactionPriority
{
    public  class TransactionPriorityManagerUnitTest
    {
        #region Private Fields

        private readonly Mock<ITransactionPriorityRepository> _transactionPriorityRepository;
        private readonly Mock<ILogger<TransactionPriorityManager>> _logger;
        private readonly IExecutionContextAccessor _executionContextAccessor;
        private readonly Mock<IUnitOfWork> _unitOfWork;
        private readonly Mock<ITransactionPrioritySmartSortRepository> _transactionPrioritySmartSortRepository;
        private readonly Mock<ISmartSortRepository> _smartSortRepository;
        private Mock<IEventBus> _eventbus;
        private readonly Mock<IOptions<Configuration>> _option;
        private readonly Mock<AutoMapper.IMapper> _mapper;
       

        #endregion

        #region Constructor

        public TransactionPriorityManagerUnitTest()
        {
            _transactionPriorityRepository = new Mock<ITransactionPriorityRepository>();
            _logger = new Mock<ILogger<TransactionPriorityManager>>();
            var tenanentKey = new Guid().ToString();
            _executionContextAccessor = new ExecutionContextAccessor() { Current = new Context() { Tenant = new TenantContext(tenanentKey) } };
            _unitOfWork = new Mock<IUnitOfWork>();
            _transactionPrioritySmartSortRepository = new Mock<ITransactionPrioritySmartSortRepository>();
            _smartSortRepository = new Mock<ISmartSortRepository>();
            _eventbus = new Mock<IEventBus>();
            _option = new Mock<IOptions<Configuration>>();
            _mapper = new Mock<IMapper>();
            _option.SetupGet(x => x.Value).Returns(new Configuration()
            {
                RoutingRule = "xyz",
                TransactionPriority = "xyz"
            });
        }

        #endregion

        #region Test Cases

        [Fact]
        public async Task  AddTransactionPriority_ForFacilityId_Positive_Test()
        {
            //Arrange
            var objTransactionPriorityPost = new TransactionPriorityPost() { PriorityCode = "TPCode", PriorityName = "TPDescription", ForManualPickFlag = true, ForManualRestockFlag = true, UseInterfaceMedNameFlag = true, ADUFlag = true, LegendForeColor = "ForeColor", LegendBackColor = "backColor", ActiveFlag = true };
            Dictionary<string, string> headers = new Dictionary<string, string>();
            List<SiteConfiguration.API.TransactionPriority.Models.TransactionPriority> listTransactionPriority = new List<SiteConfiguration.API.TransactionPriority.Models.TransactionPriority>() {};
             SiteConfiguration.API.TransactionPriority.Models.TransactionPriority objTransactionPriority = new SiteConfiguration.API.TransactionPriority.Models.TransactionPriority() {PriorityCode= "TPCodeOld", PriorityOrder=1 };
            listTransactionPriority.Add(objTransactionPriority);
            _transactionPriorityRepository.Setup(x => x.GetAll()).Returns(listTransactionPriority);
            _transactionPriorityRepository.Setup(x => x.AddAsync(It.IsAny<SiteConfiguration.API.TransactionPriority.Models.TransactionPriority>())).Returns(Task.CompletedTask);

            TransactionPriorityManager objTransactionPriorityManager = new  TransactionPriorityManager(_transactionPriorityRepository.Object, _executionContextAccessor
           , _unitOfWork.Object, _logger.Object , _transactionPrioritySmartSortRepository.Object, _smartSortRepository.Object, _eventbus.Object
                , _option.Object, _mapper.Object);

            //Act
            var response = await objTransactionPriorityManager.AddTransactionPriority(objTransactionPriorityPost,new Guid("c33e0be3-ef34-4a69-9f48-3a19de0dc649"), headers);


            Assert.Equal(response.IsSuccesss, true);
            
        }



        [Fact]
        public async Task AddTransactionPriority_CodeAlreadyExist_Positive_Test()
        {
            //Arrange
            var objTransactionPriorityPost = new TransactionPriorityPost() { PriorityCode = "TPCode", PriorityName = "TPDescription", ForManualPickFlag = true, ForManualRestockFlag = true, UseInterfaceMedNameFlag = true, ADUFlag = true, LegendForeColor = "ForeColor", LegendBackColor = "backColor", ActiveFlag = true };
            Dictionary<string, string> headers = new Dictionary<string, string>();
            List<SiteConfiguration.API.TransactionPriority.Models.TransactionPriority> listTransactionPriority = new List<SiteConfiguration.API.TransactionPriority.Models.TransactionPriority>() { };
            SiteConfiguration.API.TransactionPriority.Models.TransactionPriority objTransactionPriority = new SiteConfiguration.API.TransactionPriority.Models.TransactionPriority() { PriorityCode = "TPCode", PriorityOrder = 1 };
            listTransactionPriority.Add(objTransactionPriority);
            _transactionPriorityRepository.Setup(x => x.GetAll()).Returns(listTransactionPriority);
            _transactionPriorityRepository.Setup(x => x.AddAsync(It.IsAny<SiteConfiguration.API.TransactionPriority.Models.TransactionPriority>())).Returns(Task.CompletedTask);

            
            TransactionPriorityManager objTransactionPriorityManager = new TransactionPriorityManager(_transactionPriorityRepository.Object, _executionContextAccessor
           , _unitOfWork.Object, _logger.Object, _transactionPrioritySmartSortRepository.Object, _smartSortRepository.Object, _eventbus.Object
                , _option.Object, _mapper.Object);

            //Act
            var response = await objTransactionPriorityManager.AddTransactionPriority(objTransactionPriorityPost, new Guid("c33e0be3-ef34-4a69-9f48-3a19de0dc649"),headers);

            //Assert

            Assert.Equal(response.IsSuccesss, false);

        }

        [Fact]
        public void  GetTransactionPriorityById_Positive_Test()
        {
            //Arrange
            SiteConfiguration.API.TransactionPriority.Models.TransactionPriority objTransactionPriority = new SiteConfiguration.API.TransactionPriority.Models.TransactionPriority() { PriorityCode = "TPCodeOld", PriorityOrder = 1 };
          
            _transactionPriorityRepository.Setup(x => x.Get(Guid.Parse("c17b8f8c-ffbf-4edf-861e-ee48040e7ca7"))).Returns(objTransactionPriority);

            TransactionPriorityManager objTransactionPriorityManager = new TransactionPriorityManager(_transactionPriorityRepository.Object, _executionContextAccessor
           , _unitOfWork.Object, _logger.Object, _transactionPrioritySmartSortRepository.Object, _smartSortRepository.Object, _eventbus.Object
                , _option.Object, _mapper.Object);


            //Act
            var expected = objTransactionPriorityManager.GetTransactionPriorityById("c17b8f8c-ffbf-4edf-861e-ee48040e7ca7");

            //Assert

            Assert.Equal(expected, objTransactionPriority);

        }

        [Fact]
        public void GetTransactionPriorityById_Negative_Test()
        {
            //Arrange
            SiteConfiguration.API.TransactionPriority.Models.TransactionPriority objTransactionPriority = new SiteConfiguration.API.TransactionPriority.Models.TransactionPriority() { PriorityCode = "TPCodeOld", PriorityOrder = 1 };

            _transactionPriorityRepository.Setup(x => x.Get(Guid.Parse("c17b8f8c-ffbf-4edf-861e-ee48040e7ca7"))).Returns(objTransactionPriority);

            TransactionPriorityManager objTransactionPriorityManager = new TransactionPriorityManager(_transactionPriorityRepository.Object, _executionContextAccessor
           , _unitOfWork.Object, _logger.Object, _transactionPrioritySmartSortRepository.Object, _smartSortRepository.Object, _eventbus.Object
                , _option.Object, _mapper.Object);


            //Act
            var expected = objTransactionPriorityManager.GetTransactionPriorityById("c17b8f8c-ffbf-4edf-861e-ee48040e7ca7");

            //Assert

            Assert.NotEqual(expected, new SiteConfiguration.API.TransactionPriority.Models.TransactionPriority());

        }

        [Fact]
        public async Task UpdateTransactionPriorityAsync_TransactionPriorityId_FacilityId_TransactionPriorityObject_Positive_Test()
        {
            //Arrange
            Dictionary<string, string> headers = new Dictionary<string, string>();
            SiteConfiguration.API.TransactionPriority.Models.TransactionPriority objTransactionPriority = new SiteConfiguration.API.TransactionPriority.Models.TransactionPriority() { PriorityCode = "TPCodeOld", PriorityOrder = 1 };
            _transactionPriorityRepository.Setup(x => x.GetByTransactionPriorityAndFacilityKey(It.IsAny<Guid>(),It.IsAny<Guid>())).Returns(objTransactionPriority);
            _transactionPriorityRepository.Setup(x => x.Update(It.IsAny<SiteConfiguration.API.TransactionPriority.Models.TransactionPriority>()));

            TransactionPriorityPut objTransactionPriorityPut = new TransactionPriorityPut() { PriorityOrder = 1 };

            TransactionPriorityManager objTransactionPriorityManager = new TransactionPriorityManager(_transactionPriorityRepository.Object, _executionContextAccessor
           , _unitOfWork.Object, _logger.Object, _transactionPrioritySmartSortRepository.Object, _smartSortRepository.Object, _eventbus.Object
                , _option.Object, _mapper.Object);

            //Act
            var response = await objTransactionPriorityManager.UpdateTransactionPriorityAsync("c17b8f8c-ffbf-4edf-861e-ee48040e7ca7", objTransactionPriorityPut, new Guid("c33e0be3-ef34-4a69-9f48-3a19de0dc649"),headers);

            //Assert
            Assert.Equal(response.IsSuccesss, true);

        }

        [Fact]
        public async Task UpdateTransactionPriorityAsync_TransactionPriorityIdFacilityIdDoesnotExist_Negative_Test()
        {
            //Arrange
            Dictionary<string, string> headers = new Dictionary<string, string>();
            SiteConfiguration.API.TransactionPriority.Models.TransactionPriority objTransactionPriority = null;
            _transactionPriorityRepository.Setup(x => x.GetByTransactionPriorityAndFacilityKey(It.IsAny<Guid>(), It.IsAny<Guid>())).Returns(objTransactionPriority);
            _transactionPriorityRepository.Setup(x => x.Update(It.IsAny<SiteConfiguration.API.TransactionPriority.Models.TransactionPriority>()));

            TransactionPriorityPut objTransactionPriorityPut = new TransactionPriorityPut() { PriorityOrder = 1 };
            TransactionPriorityManager objTransactionPriorityManager = new TransactionPriorityManager(_transactionPriorityRepository.Object, _executionContextAccessor
           , _unitOfWork.Object, _logger.Object, _transactionPrioritySmartSortRepository.Object, _smartSortRepository.Object, _eventbus.Object
                , _option.Object, _mapper.Object);

            //Act
            var response = await objTransactionPriorityManager.UpdateTransactionPriorityAsync("c17b8f8c-ffbf-4edf-861e-ee48040e7ca7", objTransactionPriorityPut, new Guid("c33e0be3-ef34-4a69-9f48-3a19de0dc649"), headers);

            //Assert
            Assert.NotEqual(response.IsSuccesss, true);

        }


        [Fact]
        public async Task GetAllTransactionPriorityASync_WithRespectToOffsetLimitFacility_Posiitve_Test()
        {
            //Arrange
            List<TransactionPriorityGet> listTransactionPriorityGet = new List<TransactionPriorityGet>();
            listTransactionPriorityGet.Add(new TransactionPriorityGet() { PriorityCode = "TPCodeOld", PriorityOrder = 1 });
            IEnumerable<TransactionPriorityGet> ienumrableTransactionPriorityGet =listTransactionPriorityGet;

            List<SiteConfiguration.API.TransactionPriority.Models.TransactionPriority> listTransactionPriority = new List<SiteConfiguration.API.TransactionPriority.Models.TransactionPriority>();
            SiteConfiguration.API.TransactionPriority.Models.TransactionPriority objTransactionPriority = new SiteConfiguration.API.TransactionPriority.Models.TransactionPriority() { PriorityCode = "TPCodeOld", PriorityOrder = 1 };
            listTransactionPriority.Add(objTransactionPriority);
            Task<IEnumerable<SiteConfiguration.API.TransactionPriority.Models.TransactionPriority>> listTaskTransactionPriority = Task.FromResult<IEnumerable<SiteConfiguration.API.TransactionPriority.Models.TransactionPriority>>(listTransactionPriority);

            _transactionPriorityRepository.Setup(x => x.GetAllTransactionPriorityAsync(It.IsAny<bool>(), It.IsAny<int>(),It.IsAny<int>(),It.IsAny<Guid>())).Returns(listTaskTransactionPriority);

            TransactionPriorityManager objTransactionPriorityManager = new TransactionPriorityManager(_transactionPriorityRepository.Object, _executionContextAccessor
           , _unitOfWork.Object, _logger.Object, _transactionPrioritySmartSortRepository.Object, _smartSortRepository.Object, _eventbus.Object
                , _option.Object, _mapper.Object);

            //Act
            var response = await objTransactionPriorityManager.GetAllTransactionPriorityASync(1,1,true, "c17b8f8c-ffbf-4edf-861e-ee48040e7ca7");

            //Assert
            Assert.Equal((ienumrableTransactionPriorityGet.ToList()[0]).PriorityCode, (response.ToList()[0]).PriorityCode);
        }


        [Fact]
        public async Task GetAllTransactionPriorityASync_WithRespectToOffsetLimitFacility_NoTPIsPresent_Negative_Test()
        {
            //Arrange
            List<TransactionPriorityGet> listTransactionPriorityGet = new List<TransactionPriorityGet>();
            listTransactionPriorityGet.Add(new TransactionPriorityGet() { PriorityCode = "TPCodeOld", PriorityOrder = 1 });
            IEnumerable<TransactionPriorityGet> ienumrableTransactionPriorityGet = null;

            List<SiteConfiguration.API.TransactionPriority.Models.TransactionPriority> listTransactionPriority = null;
            Task<IEnumerable<SiteConfiguration.API.TransactionPriority.Models.TransactionPriority>> listTaskTransactionPriority = Task.FromResult<IEnumerable<SiteConfiguration.API.TransactionPriority.Models.TransactionPriority>>(listTransactionPriority); 
            _transactionPriorityRepository.Setup(x => x.GetAllTransactionPriorityAsync(It.IsAny<bool>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<Guid>())).Returns(listTaskTransactionPriority);

            TransactionPriorityManager objTransactionPriorityManager = new TransactionPriorityManager(_transactionPriorityRepository.Object, _executionContextAccessor
           , _unitOfWork.Object, _logger.Object, _transactionPrioritySmartSortRepository.Object, _smartSortRepository.Object, _eventbus.Object
                , _option.Object, _mapper.Object);

            //Act
            var response = await objTransactionPriorityManager.GetAllTransactionPriorityASync(1, 1, true, "c17b8f8c-ffbf-4edf-861e-ee48040e7ca7");

            //Assert
            Assert.Equal(ienumrableTransactionPriorityGet, response);
        }

        [Fact]
        public async Task GetAllSerachedTransactionPriorityAsync_WithRespectToOffsetLimitFacility_Posiitve_Test()
        {
            //Arrange
            List<TransactionPriorityGet> listTransactionPriorityGet = new List<TransactionPriorityGet>();
            listTransactionPriorityGet.Add(new TransactionPriorityGet() { PriorityCode = "TPCodeOld", PriorityOrder = 1 });
            IEnumerable<TransactionPriorityGet> ienumrableTransactionPriorityGet = listTransactionPriorityGet;

            List<SiteConfiguration.API.TransactionPriority.Models.TransactionPriority> listTransactionPriority = new List<SiteConfiguration.API.TransactionPriority.Models.TransactionPriority>();
            SiteConfiguration.API.TransactionPriority.Models.TransactionPriority objTransactionPriority = new SiteConfiguration.API.TransactionPriority.Models.TransactionPriority() { PriorityCode = "TPCodeOld", PriorityOrder = 1 };
            listTransactionPriority.Add(objTransactionPriority);
            Task<IEnumerable<SiteConfiguration.API.TransactionPriority.Models.TransactionPriority>> listTaskTransactionPriority = Task.FromResult<IEnumerable<SiteConfiguration.API.TransactionPriority.Models.TransactionPriority>>(listTransactionPriority);

            _transactionPriorityRepository.Setup(x => x.GetAllSerachedTransactionPriorityAsync(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<Guid>())).Returns(listTaskTransactionPriority);

            TransactionPriorityManager objTransactionPriorityManager = new TransactionPriorityManager(_transactionPriorityRepository.Object, _executionContextAccessor
           , _unitOfWork.Object, _logger.Object, _transactionPrioritySmartSortRepository.Object, _smartSortRepository.Object, _eventbus.Object
                , _option.Object, _mapper.Object);

            //Act
            var response = await objTransactionPriorityManager.GetAllSerachedTransactionPriorityAsync("TPDescription", 1, 1, "c17b8f8c-ffbf-4edf-861e-ee48040e7ca7");

            //Assert
            Assert.Equal((ienumrableTransactionPriorityGet.ToList()[0]).PriorityCode, (response.ToList()[0]).PriorityCode);

        }

        [Fact]
        public async Task GetAllSerachedTransactionPriorityAsync_WithRespectToOffsetLimitFacility_NoTPIsPresent_Negative_Test()
        {
            //Arrange
            List<TransactionPriorityGet> listTransactionPriorityGet = new List<TransactionPriorityGet>();
            listTransactionPriorityGet.Add(new TransactionPriorityGet() { PriorityCode = "TPCodeOld", PriorityOrder = 1 });
            IEnumerable<TransactionPriorityGet> ienumrableTransactionPriorityGet = null;

            List<SiteConfiguration.API.TransactionPriority.Models.TransactionPriority> listTransactionPriority = null;
            Task<IEnumerable<SiteConfiguration.API.TransactionPriority.Models.TransactionPriority>> listTaskTransactionPriority = Task.FromResult<IEnumerable<SiteConfiguration.API.TransactionPriority.Models.TransactionPriority>>(listTransactionPriority);
            _transactionPriorityRepository.Setup(x => x.GetAllSerachedTransactionPriorityAsync(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<Guid>())).Returns(listTaskTransactionPriority);

            TransactionPriorityManager objTransactionPriorityManager = new TransactionPriorityManager(_transactionPriorityRepository.Object, _executionContextAccessor
           , _unitOfWork.Object, _logger.Object, _transactionPrioritySmartSortRepository.Object, _smartSortRepository.Object, _eventbus.Object
                , _option.Object, _mapper.Object);

            //Act
            var response = await objTransactionPriorityManager.GetAllSerachedTransactionPriorityAsync("TPDescription", 1, 1, "c17b8f8c-ffbf-4edf-861e-ee48040e7ca7");

            //Assert
            Assert.Equal(ienumrableTransactionPriorityGet, response);
        }


        [Fact]
        public async Task GetSmartSortForTransactionPriority_ForTransactionPriorityKeyfacilityIDId_NoTransactionPriorityExist_Negative_Test()
        {
            //Arrange
            SiteConfiguration.API.TransactionPriority.Models.TransactionPriority objTransactionPriority = null; //new SiteConfiguration.API.TransactionPriority.Models.TransactionPriority() { PriorityCode = "TPCodeOld", PriorityOrder = 1 };
            _transactionPriorityRepository.Setup(x => x.GetByTransactionPriorityAndFacilityKey(It.IsAny<Guid>(), It.IsAny<Guid>())).Returns(objTransactionPriority);
            
            IEnumerable<TransactionPrioritySmartSort> transactionPrioritySmartSortsExpected = null;
            TransactionPriorityManager objTransactionPriorityManager = new TransactionPriorityManager(_transactionPriorityRepository.Object, _executionContextAccessor
          , _unitOfWork.Object, _logger.Object, _transactionPrioritySmartSortRepository.Object, _smartSortRepository.Object, _eventbus.Object
                , _option.Object, _mapper.Object);

            //Act
            var response = await objTransactionPriorityManager.GetSmartSortForTransactionPriority("c17b8f8c-ffbf-4edf-861e-ee48040e7ca7", "c17b8f8c-ffbf-4edf-861e-ee48040e7ca7");

            //Assert
            Assert.Equal(transactionPrioritySmartSortsExpected, response);
        }

        [Fact]
        public async Task GetSmartSortForTransactionPriority_ForTransactionPriorityKeyfacilityIDId_NotTransactionPrioritySmartSortExist_Negative_Test()
        {
            //Arrange
            SiteConfiguration.API.TransactionPriority.Models.TransactionPriority objTransactionPriority = new SiteConfiguration.API.TransactionPriority.Models.TransactionPriority() { PriorityCode = "TPCodeOld", PriorityOrder = 1 };
            _transactionPriorityRepository.Setup(x => x.GetByTransactionPriorityAndFacilityKey(It.IsAny<Guid>(), It.IsAny<Guid>())).Returns(objTransactionPriority);

            IEnumerable<TransactionPrioritySmartSort> listTransactionPrioritySmartSort = null;
            Task<IEnumerable<TransactionPrioritySmartSort>> listTaskTransactionPrioritySmartSort = Task.FromResult<IEnumerable<TransactionPrioritySmartSort>>(listTransactionPrioritySmartSort);

            _transactionPrioritySmartSortRepository.Setup(x => x.GetSmartSortForTransactionPriorityAsync(It.IsAny<Guid>())).Returns(listTaskTransactionPrioritySmartSort);

            TransactionPriorityManager objTransactionPriorityManager = new TransactionPriorityManager(_transactionPriorityRepository.Object, _executionContextAccessor
          , _unitOfWork.Object, _logger.Object, _transactionPrioritySmartSortRepository.Object, _smartSortRepository.Object, _eventbus.Object
                , _option.Object, _mapper.Object);

            IEnumerable<TransactionPrioritySmartSort> transactionPrioritySmartSortsExpected = null;

            //Act
            var response = await objTransactionPriorityManager.GetSmartSortForTransactionPriority("c17b8f8c-ffbf-4edf-861e-ee48040e7ca7", "c17b8f8c-ffbf-4edf-861e-ee48040e7ca7");

            //Assert
            Assert.Equal(transactionPrioritySmartSortsExpected, response);
        }


        [Fact]
        public async Task GetSmartSortForTransactionPriority_ForTransactionPriorityKeyfacilityIDId_TransactionPrioritySmartSortExist_Positive_Test()
        {
            //Arrange
            SiteConfiguration.API.TransactionPriority.Models.TransactionPriority objTransactionPriority = new SiteConfiguration.API.TransactionPriority.Models.TransactionPriority() { PriorityCode = "TPCodeOld", PriorityOrder = 1 };
            _transactionPriorityRepository.Setup(x => x.GetByTransactionPriorityAndFacilityKey(It.IsAny<Guid>(), It.IsAny<Guid>())).Returns(objTransactionPriority);

            IEnumerable<TransactionPrioritySmartSort> listTransactionPrioritySmartSort = new List<TransactionPrioritySmartSort>();
            Task<IEnumerable<TransactionPrioritySmartSort>> listTaskTransactionPrioritySmartSort = Task.FromResult<IEnumerable<TransactionPrioritySmartSort>>(listTransactionPrioritySmartSort);

            _transactionPrioritySmartSortRepository.Setup(x => x.GetSmartSortForTransactionPriorityAsync(It.IsAny<Guid>())).Returns(listTaskTransactionPrioritySmartSort);

            TransactionPriorityManager objTransactionPriorityManager = new TransactionPriorityManager(_transactionPriorityRepository.Object, _executionContextAccessor
          , _unitOfWork.Object, _logger.Object, _transactionPrioritySmartSortRepository.Object, _smartSortRepository.Object, _eventbus.Object
                , _option.Object, _mapper.Object);

            IEnumerable<TransactionPrioritySmartSort> transactionPrioritySmartSortsExpected = null;

            //Act
            var response = await objTransactionPriorityManager.GetSmartSortForTransactionPriority("c17b8f8c-ffbf-4edf-861e-ee48040e7ca7", "c17b8f8c-ffbf-4edf-861e-ee48040e7ca7");

            //Assert
            Assert.Equal(listTransactionPrioritySmartSort, response);
        }


        [Fact]
        public async Task PutSmartSortForTransactionPriorityAsync_NoTransactionPriorityExist_Negative_Test()
        {
          //  //Arrange
          //  SiteConfiguration.API.TransactionPriority.Models.TransactionPriority objTransactionPriority = null; 
          //  _transactionPriorityRepository.Setup(x => x.GetByTransactionPriorityAndFacilityKey(It.IsAny<Guid>(), It.IsAny<Guid>())).Returns(objTransactionPriority);

          //  bool flagExpected =false;
          //  TransactionPriorityManager objTransactionPriorityManager = new TransactionPriorityManager(_transactionPriorityRepository.Object, _executionContextAccessor
          //, _unitOfWork.Object, _logger.Object, _transactionPrioritySmartSortRepository.Object, _smartSortRepository.Object);

          //  //Act
          //  var response = await objTransactionPriorityManager.PutSmartSortForTransactionPriorityAsync("c17b8f8c-ffbf-4edf-861e-ee48040e7ca7", "c17b8f8c-ffbf-4edf-861e-ee48040e7ca7", new List<TransactionPrioritySmartSortPut>());

          //  //Assert
          //  Assert.Equal(flagExpected, response);
        }

        [Fact]
        public async Task PutSmartSortForTransactionPriorityAsync_TransactionPriorityExist_Positive_Test()
        {
          //  //Arrange
          //  SiteConfiguration.API.TransactionPriority.Models.TransactionPriority objTransactionPriority = new SiteConfiguration.API.TransactionPriority.Models.TransactionPriority() { PriorityCode = "TPCodeOld", PriorityOrder = 1 };
          //  _transactionPriorityRepository.Setup(x => x.GetByTransactionPriorityAndFacilityKey(It.IsAny<Guid>(), It.IsAny<Guid>())).Returns(objTransactionPriority);

          //  bool flagExpected = true;

          //  _transactionPrioritySmartSortRepository.Setup(x=>x.PutSmartSortForTransactionPriorityAsync(new Guid(), new List<SmartSort>())).Returns(Task.CompletedTask);

          //  TransactionPriorityManager objTransactionPriorityManager = new TransactionPriorityManager(_transactionPriorityRepository.Object, _executionContextAccessor
          //, _unitOfWork.Object, _logger.Object, _transactionPrioritySmartSortRepository.Object, _smartSortRepository.Object);

          //  //Act
          //  var response = await objTransactionPriorityManager.PutSmartSortForTransactionPriorityAsync("c17b8f8c-ffbf-4edf-861e-ee48040e7ca7", "c17b8f8c-ffbf-4edf-861e-ee48040e7ca7", new List<TransactionPrioritySmartSortPut>() { new TransactionPrioritySmartSortPut() { SmartSortId= "c17b8f8c-ffbf-4edf-861e-ee48040e7ca7", TPSortOrder=1 } });

          //  //Assert
          //  Assert.Equal(flagExpected, response);
        }

        #endregion
    }
}

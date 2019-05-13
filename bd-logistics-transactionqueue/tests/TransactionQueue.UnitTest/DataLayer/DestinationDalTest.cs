//using AutoMapper;
//using Moq;
//using System.Threading.Tasks;
//using TransactionQueue.API.DataLayer;
//using TransactionQueue.API.Infrastructure.Repository.Interfaces;
//using Xunit;

//namespace TransactionQueue.UnitTest.DataLayer
//{
//    /// <summary>
//    /// Unit Tests for Destination Dal class methods.
//    /// </summary>
//    public class DestinationDalTest
//    {
//        #region Private Fields
//        private readonly Mock<IDestinationMongoRepository> fakeDestinationMongoRepository;
//        private DestinationDal destinationDal;
//        private readonly IMapper _mapper;
//        #endregion

//        #region Constructor
//        public DestinationDalTest()
//        {
//            var mockMapper = new MapperConfiguration(cfg =>
//            {
//                cfg.AddProfile(new API.AutoMapper.MapProfile());
//            });
//            _mapper = mockMapper.CreateMapper();

//            fakeDestinationMongoRepository = new Mock<IDestinationMongoRepository>();
//            destinationDal = new DestinationDal(fakeDestinationMongoRepository.Object, _mapper);
//        }
//        #endregion

//        #region Test Cases
//        [Fact]
//        public async Task DestinationMongoRepository_Should_Call_GetDestinationByCode()
//        {
//            var destinationCode = "fortis";
//            //Act
//            await destinationDal.GetDestinationByCode(destinationCode);

//            //Assert
//            fakeDestinationMongoRepository.Verify(x => x.GetDestinationByCode(destinationCode), Times.Once);
//        }

//        #endregion
//    }
//}
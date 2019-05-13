//using AutoMapper;
//using Moq;
//using System.Threading.Tasks;
//using TransactionQueue.API.DataLayer;
//using TransactionQueue.API.Infrastructure.Repository.Interfaces;
//using Xunit;

//namespace TransactionQueue.UnitTest.DataLayer
//{
//    /// <summary>
//    /// Unit Tests for LastAduXref Dal class methods.
//    /// </summary>
//    public class LastAduXrefDalTest
//    {
//        #region Private Fields
//        private readonly Mock<ILastAduXrefMongoRepository> fakeLastAduXrefMongoRepository;
//        private LastAduXrefDal lastAduXrefDal;
//        private readonly IMapper _mapper;
//        #endregion

//        #region Constructor
//        public LastAduXrefDalTest()
//        {
//            var mockMapper = new MapperConfiguration(cfg =>
//            {
//                cfg.AddProfile(new API.AutoMapper.MapProfile());
//            });
//            _mapper = mockMapper.CreateMapper();

//            fakeLastAduXrefMongoRepository = new Mock<ILastAduXrefMongoRepository>();
//            lastAduXrefDal = new LastAduXrefDal(fakeLastAduXrefMongoRepository.Object, _mapper);
//        }
//        #endregion

//        #region Test Cases
//        [Fact]
//        public async Task LastAduXrefMongoRepository_Should_Call_GetAllLastAduXrefTransactions()
//        {
//            //Act
//            await lastAduXrefDal.GetAllLastAduXrefTransactions();

//            //Assert
//            fakeLastAduXrefMongoRepository.Verify(x => x.GetAllLastAduXrefTransactions(), Times.Once);
//        }

//        #endregion
//    }
//}
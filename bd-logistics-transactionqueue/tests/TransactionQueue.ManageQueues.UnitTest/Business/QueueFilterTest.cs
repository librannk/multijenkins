
using Moq;
using Xunit;
using TransactionQueue.ManageQueues.QueueHelper;
using TransactionQueue.ManageQueues.Business.Concrete;
using TransactionQueue.ManageQueues.UnitTest.TestData;
using TransactionQueue.ManageQueues.Business.Abstraction;

namespace TransactionQueue.ManageQueues.UnitTest.Business
{
    public class QueueFilterTest
    {
        [Theory]
        [InlineData("CKHEPARIN 25,000 units/500mL", "BKEYS- PCA/EPIDURAL", "AKEYS- PCA/EPIDURAL")]
        //Summary StoryId <> Default Sort Functionality by Status and Type
        public void Test_DefaultSorting_OrderByColumnNameDesc(string description1, string description2, string description3)
        {
            var tq = new Mock<ITransactionQueueRepository>();
            var tp = new Mock<IPriorityRules>();
            var mock = new Mock<ISortStrategy>();
            //var ISA = new Mock<List<int?>>();
            string sortedColumnName = "Description";
            int sortedDirection = 1;
            int page = 1;
            int pageSize = 100;
            TestDataGeneratorQueueFilter testDataGenerator = new TestDataGeneratorQueueFilter();
            var transactionQueueEntities = testDataGenerator.GetTransactionQueueEntityByColumn();
            var transactionPriorities = testDataGenerator.GetTransactionPriorityEntity();

            //tq.Setup(x => x.GetTransactionsByISAId(ISA.Object)).Returns(transactionQueueEntities);
            tp.Setup(x => x.GetPriorityRules()).Returns(transactionPriorities);

            IQueueFilter queueFilter = new QueueFilter(tp.Object, tq.Object);
            SortedQueue sortedqueue = queueFilter.GetAllSortedTransaction(transactionQueueEntities, sortedColumnName, sortedDirection, page, pageSize);
            Assert.Equal(description1, sortedqueue.QueueList[0].Description);
            Assert.Equal(description2, sortedqueue.QueueList[1].Description);
            Assert.Equal(description3, sortedqueue.QueueList[2].Description);
        }
        [Theory]
        [InlineData("AKEYS- PCA/EPIDURAL", "BKEYS- PCA/EPIDURAL", "CKHEPARIN 25,000 units/500mL")]
        //Summary StoryId <> Default Sort Functionality by Status and Type
        public void Test_DefaultSorting_OrderByColumnNameAsc(string description1, string description2, string description3)
        {
            var tq = new Mock<ITransactionQueueRepository>();
            var tp = new Mock<IPriorityRules>();
            var mock = new Mock<ISortStrategy>();
            //var ISA = new Mock<List<int?>>();
            string sortedColumnName = "Description";
            int sortedDirection = 0;
            int page = 1;
            int pageSize = 10;
            TestDataGeneratorQueueFilter testDataGenerator = new TestDataGeneratorQueueFilter();
            var transactionQueueEntities = testDataGenerator.GetTransactionQueueEntityByColumn();
            var transactionPriorities = testDataGenerator.GetTransactionPriorityEntity();

            //tq.Setup(x => x.GetTransactionsByISAId(ISA.Object)).Returns(transactionQueueEntities);
            tp.Setup(x => x.GetPriorityRules()).Returns(transactionPriorities);

            IQueueFilter queueFilter = new QueueFilter(tp.Object, tq.Object);
            SortedQueue sortedqueue = queueFilter.GetAllSortedTransaction(transactionQueueEntities, sortedColumnName, sortedDirection, page, pageSize);
            Assert.Equal(description1, sortedqueue.QueueList[0].Description);
            Assert.Equal(description2, sortedqueue.QueueList[1].Description);
            Assert.Equal(description3, sortedqueue.QueueList[2].Description);
        }
        [Theory]
        [InlineData(6, 7, 8)]
        //Summary StoryId <> Default Sort Functionality by Status and Type
        public void Test_DefaultSorting_OrderBySmartCol_TransactionPriorityOrder(int status1, int status2, int status3)
        {
            var tq = new Mock<ITransactionQueueRepository>();
            var tp = new Mock<IPriorityRules>();
            var mock = new Mock<ISortStrategy>();
            //var ISA = new Mock<List<int?>>();
            string sortedColumnName = string.Empty;
            int sortedDirection = 1;
            int page = 1;
            int pageSize = 10;
            TestDataGeneratorQueueFilter testDataGenerator = new TestDataGeneratorQueueFilter();
            var transactionQueueEntities = testDataGenerator.GetTransactionQueueEntityByColumn();
            var transactionPriorities = testDataGenerator.GetTransactionPriorityEntity();

            //tq.Setup(x => x.GetTransactionsByISAId(ISA.Object)).Returns(transactionQueueEntities);
            tp.Setup(x => x.GetPriorityRules()).Returns(transactionPriorities);

            IQueueFilter queueFilter = new QueueFilter(tp.Object, tq.Object);
            SortedQueue sortedqueue = queueFilter.GetAllSortedTransaction(transactionQueueEntities, sortedColumnName, sortedDirection, page, pageSize);
            Assert.Equal(status1, sortedqueue.QueueList[0].TransactionPriorityOrder);
            Assert.Equal(status2, sortedqueue.QueueList[1].TransactionPriorityOrder);
            Assert.Equal(status3, sortedqueue.QueueList[2].TransactionPriorityOrder);
        }
        [Theory]
        [InlineData("FallingRack1", "RollingRack2", "SillingRack3")]
        //Summary StoryId <> Default Sort Functionality by Status and Type
        public void Test_DefaultSorting_OrderBySmartColLocation(string PatientName1, string PatientName2, string PatientName3)
        {
            var tq = new Mock<ITransactionQueueRepository>();
            var tp = new Mock<IPriorityRules>();
            var mock = new Mock<ISortStrategy>();
            //var ISA = new Mock<List<int?>>();
            string sortedColumnName = string.Empty;
            int sortedDirection = 1;
            int page = 1;
            int pageSize = 10;
            TestDataGeneratorQueueFilter testDataGenerator = new TestDataGeneratorQueueFilter();
            var transactionQueueEntities = testDataGenerator.GetTransactionQueueEntityByColumn();
            var transactionPriorities = testDataGenerator.GetTransactionPriorityEntity();

            //tq.Setup(x => x.GetTransactionsByISAId(ISA.Object)).Returns(transactionQueueEntities);
            tp.Setup(x => x.GetPriorityRules()).Returns(transactionPriorities);

            IQueueFilter queueFilter = new QueueFilter(tp.Object, tq.Object);
            SortedQueue sortedqueue = queueFilter.GetAllSortedTransaction(transactionQueueEntities, sortedColumnName, sortedDirection, page, pageSize);
            Assert.Equal(PatientName1, sortedqueue.QueueList[0].Location);
            Assert.Equal(PatientName2, sortedqueue.QueueList[1].Location);
            Assert.Equal(PatientName3, sortedqueue.QueueList[2].Location);
        }
        [Fact]
        public void Test_GetSortedTransaction_HavingAnActiveTransaction()
        {
            var transactionQueue = new Mock<ITransactionQueueRepository>();
            var sortStrategy = new Mock<ISortStrategy>();
            var Ipriority = new Mock<IPriorityRules>();

            TestDataGeneratorTransaction testData = new TestDataGeneratorTransaction();
            var activepending = testData.GetPendingTransaction();
            var active = testData.GetActiveTransaction();
            var hold = testData.GetHoldTransaction();
            var priority = testData.GetTransactionPriority();

            Ipriority.Setup(x => x.GetPriorityRules()).Returns(priority);

            var queueFilter = new QueueFilter(Ipriority.Object, transactionQueue.Object);
            
        }
    }
}

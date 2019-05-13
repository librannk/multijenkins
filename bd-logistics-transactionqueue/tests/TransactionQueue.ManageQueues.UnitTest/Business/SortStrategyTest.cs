
using Moq;
using Xunit;
using System;
using System.Collections.Generic;
using TransactionQueue.ManageQueues.QueueHelper;
using TransactionQueue.ManageQueues.Business.Concrete;
using TransactionQueue.ManageQueues.UnitTest.TestData;
using TransactionQueue.ManageQueues.Business.Abstraction;

namespace TransactionQueue.ManageQueues.UnitTest.Business
{
    public class SortStrategyTest
    {
        [Theory]
        [InlineData("R", "P", "A")]
        //Summary StoryId <> Default Sort Functionality by Status and Type
        public void Test_DefaultSorting_OrderByStatus_Type(string status1, string status2, string status3)
        {
            var tq = new Mock<ITransactionQueueRepository>();
            var tp = new Mock<IPriorityRules>();
            var mock = new Mock<ISortStrategy>();
            //var ISA = new Mock<List<int?>>();
            string sortedColumnName = "Description";
            int sortedDirection = 1;
            int page = 1;
            int pageSize = 10;
            TestDataGeneratorSortStrategy testDataGenerator = new TestDataGeneratorSortStrategy();
            var transactionQueueEntities = testDataGenerator.GetTransactionQueueEntity();
            var transactionPriorities = testDataGenerator.GetTransactionPriorityEntity();

            //tq.Setup(x => x.GetTransactionsByISAId(ISA.Object)).Returns(transactionQueueEntities);
            tp.Setup(x => x.GetPriorityRules()).Returns(transactionPriorities);

            ISortStrategy tranQueueData = new ColumnSorting(tp.Object, tq.Object);
            SortedQueue sortedqueue = tranQueueData.GetSortedQueue(transactionQueueEntities, sortedColumnName, sortedDirection, page, pageSize);
            Assert.Equal(status1, sortedqueue.QueueList[0].Status);
            Assert.Equal(status2, sortedqueue.QueueList[1].Status);
            Assert.Equal(status3, sortedqueue.QueueList[2].Status);
        }
        [Theory]
        [InlineData("Mark", "Joe", "Bill")]
        //Summary StoryId <> Default Sort Functionality by PatientName
        public void Test_DefaultSorting_OrderByPatientName(string patientOrder1, string patientOrder2, string patientOrder3)
        {
            var tq = new Mock<ITransactionQueueRepository>();
            var tp = new Mock<IPriorityRules>();
            var mock = new Mock<ISortStrategy>();
            //var ISA = new Mock<List<int?>>();
            string sortedColumnName = "PatientName";
            int sortedDirection = 1;
            int page = 1;
            int pageSize = 10;
            TestDataGeneratorSortStrategy testDataGenerator = new TestDataGeneratorSortStrategy();
            var transactionQueueEntities = testDataGenerator.GetTransactionQueueEntitybyPatient();
            var transactionPriorities = testDataGenerator.GetTransactionPriorityEntity();

            //tq.Setup(x => x.GetTransactionsByISAId(ISA.Object)).Returns(transactionQueueEntities);
            tp.Setup(x => x.GetPriorityRules()).Returns(transactionPriorities);

            ISortStrategy tranQueueData = new ColumnSorting(tp.Object, tq.Object);
            SortedQueue sortedqueue = tranQueueData.GetSortedQueue(transactionQueueEntities, sortedColumnName, sortedDirection, page, pageSize);
            Assert.Equal(patientOrder1, sortedqueue.QueueList[0].PatientName);
            Assert.Equal(patientOrder2, sortedqueue.QueueList[1].PatientName);
            Assert.Equal(patientOrder3, sortedqueue.QueueList[2].PatientName);
        }
        public static IEnumerable<object[]> Data =>
        new List<object[]>
        {
            new object[] { DateTime.Today, DateTime.Today.AddDays(+1), DateTime.Today.AddDays(+2) }
        };
        [Theory]
        [MemberData(nameof(Data))]
        //Summary StoryId <> Default Sort Functionality by ReceivedDT
        public void Test_DefaultSorting_OrderByReceivedDT(DateTime ReceivedDT1, DateTime ReceivedDT2, DateTime ReceivedDT3)
        {
            var tq = new Mock<ITransactionQueueRepository>();
            var tp = new Mock<IPriorityRules>();
            var mock = new Mock<ISortStrategy>();
            //var ISA = new Mock<List<int?>>();
            string sortedColumnName = "PatientName";
            int sortedDirection = 1;
            int page = 1;
            int pageSize = 10;
            TestDataGeneratorSortStrategy testDataGenerator = new TestDataGeneratorSortStrategy();
            var transactionQueueEntities = testDataGenerator.GetTransactionQueueEntitybyReceivedDT();
            var transactionPriorities = testDataGenerator.GetTransactionPriorityEntity();

            //tq.Setup(x => x.GetTransactionsByISAId(ISA.Object)).Returns(transactionQueueEntities);
            tp.Setup(x => x.GetPriorityRules()).Returns(transactionPriorities);

            ISortStrategy tranQueueData = new ColumnSorting(tp.Object, tq.Object);
            SortedQueue sortedqueue = tranQueueData.GetSortedQueue(transactionQueueEntities, sortedColumnName, sortedDirection, page, pageSize);
            Assert.Equal(ReceivedDT1, sortedqueue.QueueList[0].ReceivedDT);
            Assert.Equal(ReceivedDT2, sortedqueue.QueueList[1].ReceivedDT);
            Assert.Equal(ReceivedDT3, sortedqueue.QueueList[2].ReceivedDT);
        }

        [Theory]
        [InlineData("FallingRack1", "RollingRack2", "SillingRack3")]
        //Summary StoryId <> Default Sort Functionality by Location
        public void Test_SmartSorting_OrderByLocation(string location1, string location2, string location3)
        {
            var tq = new Mock<ITransactionQueueRepository>();
            var tp = new Mock<IPriorityRules>();
            var mock = new Mock<ISortStrategy>();
            //var ISA = new Mock<List<int?>>();
            string sortedColumnName = null;
            int sortedDirection = 1;
            int page = 1;
            int pageSize = 10;
            TestDataGeneratorSortStrategy testDataGenerator = new TestDataGeneratorSortStrategy();
            var transactionQueueEntities = testDataGenerator.GetTransactionQueueEntityBySmartColumn();
            var transactionPriorities = testDataGenerator.GetTransactionPriorityEntityBySmartColumn();

            //tq.Setup(x => x.GetTransactionsByISAId(ISA.Object)).Returns(transactionQueueEntities);
            tp.Setup(x => x.GetPriorityRules()).Returns(transactionPriorities);

            SmartSorting tranQueueData = new SmartSorting(tp.Object, tq.Object);
            SortedQueue sortedqueue = tranQueueData.GetSortedQueue(transactionQueueEntities, sortedColumnName, sortedDirection, page, pageSize);
            Assert.Equal(location1, sortedqueue.QueueList[0].Location);
            Assert.Equal(location2, sortedqueue.QueueList[1].Location);
            Assert.Equal(location3, sortedqueue.QueueList[2].Location);
        }
        [Theory]
        [InlineData(6, 7, 8)]
        //Summary StoryId <> Default Sort Functionality by Destination
        public void Test_SmartSorting_OrderByTransactionPriorityOrder(int priorityOrder1, int priorityOrder2, int priorityOrder3)
        {
            var tq = new Mock<ITransactionQueueRepository>();
            var tp = new Mock<IPriorityRules>();
            var mock = new Mock<ISortStrategy>();
            //var ISA = new Mock<List<int?>>();
            string sortedColumnName = null;
            int sortedDirection = 1;
            int page = 1;
            int pageSize = 10;
            TestDataGeneratorSortStrategy testDataGenerator = new TestDataGeneratorSortStrategy();
            var transactionQueueEntities = testDataGenerator.GetTransactionQueueEntityBySmartColumn();
            var transactionPriorities = testDataGenerator.GetTransactionPriorityEntityByDestination();

            //tq.Setup(x => x.GetTransactionsByISAId(ISA.Object)).Returns(transactionQueueEntities);
            tp.Setup(x => x.GetPriorityRules()).Returns(transactionPriorities);

            SmartSorting tranQueueData = new SmartSorting(tp.Object, tq.Object);
            SortedQueue sortedqueue = tranQueueData.GetSortedQueue(transactionQueueEntities, sortedColumnName, sortedDirection, page, pageSize);
            Assert.Equal(priorityOrder1, sortedqueue.QueueList[0].TransactionPriorityOrder);
            Assert.Equal(priorityOrder2, sortedqueue.QueueList[1].TransactionPriorityOrder);
            Assert.Equal(priorityOrder3, sortedqueue.QueueList[2].TransactionPriorityOrder);
        }
        [Theory]
        [InlineData("Bill", "Joe", "Mark")]
        //Summary StoryId <> Default Sort Functionality by PatientName
        public void Test_SmartSorting_OrderByPatientName(string patient1, string patient2, string patient3)
        {
            var tq = new Mock<ITransactionQueueRepository>();
            var tp = new Mock<IPriorityRules>();
            var mock = new Mock<ISortStrategy>();
            //var ISA = new Mock<List<int?>>();
            string sortedColumnName = null;
            int sortedDirection = 1;
            int page = 1;
            int pageSize = 10;
            TestDataGeneratorSortStrategy testDataGenerator = new TestDataGeneratorSortStrategy();
            var transactionQueueEntities = testDataGenerator.GetTransactionQueueEntityBySmartColumn();
            var transactionPriorities = testDataGenerator.GetTransactionPriorityEntityByPatientName();

            //tq.Setup(x => x.GetTransactionsByISAId(ISA.Object)).Returns(transactionQueueEntities);
            tp.Setup(x => x.GetPriorityRules()).Returns(transactionPriorities);

            SmartSorting tranQueueData = new SmartSorting(tp.Object, tq.Object);
            SortedQueue sortedqueue = tranQueueData.GetSortedQueue(transactionQueueEntities, sortedColumnName, sortedDirection, page, pageSize);
            Assert.Equal(patient1, sortedqueue.QueueList[0].PatientName);
            Assert.Equal(patient2, sortedqueue.QueueList[1].PatientName);
            Assert.Equal(patient3, sortedqueue.QueueList[2].PatientName);
        }
    }
}

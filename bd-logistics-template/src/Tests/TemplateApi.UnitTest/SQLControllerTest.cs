using System;
using System.Collections.Generic;
using BD.Template.API.Controllers;
using BD.Template.API.Infrastructure.DataAccess.SQL.DBContextEntities.Entities;
using BD.Template.API.Infrastructure.DataAccess.SQL.DBContextEntities.EntityRepositoryContracts;
using BD.Template.API.Infrastructure.DataAccess.SQL.DBContextEntities.UnitOfWork;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Moq;
using Xunit;

namespace TemplateApi.UnitTest
{
    public class SampleSqlControllerTest
    {
        Mock<ICustomerRepository> _customerRepo;
        Mock<IUnitOfWork> _unitOfWork;
        Mock<IDistributedCache> _cache;
        private void Initialize()
        {
            _customerRepo = new Mock<ICustomerRepository>();
            _unitOfWork = new Mock<IUnitOfWork>();
            _cache = new Mock<IDistributedCache>();
        }

        [Fact]
        public async void Test_Get_Positive_Response()
        {
            try
            {
                Initialize();
                var customerObj = new Customer() { Id = 1, Name = "Sam" };
                List<Customer> listCustomer = new List<Customer>();
                listCustomer.Add(customerObj);
                _customerRepo.Setup(x => x.GetAll()).Returns(listCustomer);
                SampleSqlController controller = new SampleSqlController(_customerRepo.Object, _unitOfWork.Object, _cache.Object);

                var expected = await controller.Get() as ActionResult<Customer>;

                Assert.Null(expected.Value);
            }
            catch (Exception ex)
            {
                Assert.False(true, "Test case failed: " + ex.Message);
            }
        }


        [Fact]
        public async void Test_Get_Negative_Response()
        {
            try
            {
                Initialize();
                var customerObj =new Customer() { Id = 1, Name = "Sam" };
                List<Customer> listCustomer = new List<Customer>();
                listCustomer.Add(customerObj);
                listCustomer = null;
                _customerRepo.Setup(x => x.GetAll()).Returns(listCustomer);
                SampleSqlController controller = new SampleSqlController(_customerRepo.Object, _unitOfWork.Object, _cache.Object);

                var expected = await controller.Get() as ActionResult<Customer>;

                Assert.Null(expected.Value);
            }
            catch (Exception ex)
            {
                Assert.False(true, "Test case failed: " + ex.Message);
            }
        }

        [Fact]
        public void Test_GetById_Positive_Response()
        {
            try
            {
                Initialize();
                var customerObj = new Customer() { Id = 1, Name = "Sam" };
                List<Customer> listCustomer = new List<Customer>();
                listCustomer.Add(customerObj);
                _customerRepo.Setup(x => x.GetAll()).Returns(listCustomer);
                SampleSqlController controller = new SampleSqlController(_customerRepo.Object, _unitOfWork.Object, _cache.Object);

                var expected = controller.Get(1);

                Assert.Equal(expected, customerObj);
            }
            catch (Exception ex)
            {
                Assert.False(true, "Test case failed: " + ex.Message);
            }
        }

        [Fact]
        public void Test_GetById_Negative_Response()
        {
            try
            {
                Initialize();
                var customerObj = new Customer() { Id = 1, Name = "Sam" };
                List<Customer> listCustomer = new List<Customer>();
                listCustomer.Add(customerObj);
                _customerRepo.Setup(x => x.GetAll()).Returns(listCustomer);
                SampleSqlController controller = new SampleSqlController(_customerRepo.Object, _unitOfWork.Object, _cache.Object);

                var expected = controller.Get(1);

                Assert.NotEqual(expected, new Customer());
            }
            catch (Exception ex)
            {
                Assert.False(true, "Test case failed: " + ex.Message);
            }
        }

        [Fact]
        public void Test_Post_Positive_Response()
        {
            try
            {
                Initialize();
                _customerRepo.Setup(x => x.Add(It.IsAny<Customer>()));
                SampleSqlController controller = new SampleSqlController(_customerRepo.Object, _unitOfWork.Object, _cache.Object);

                controller.Post(new Customer());

                Assert.True(true);
            }
            catch (Exception ex)
            {
                Assert.False(true, "Test case failed: " + ex.Message);
            }
        }

        [Fact]
        public void Test_Delete_Positive_Response()
        {
            try
            {
                Initialize();
                var customerObj = new Customer() { Id = 1, Name = "Sam" };
                List<Customer> listCustomer = new List<Customer>();
                listCustomer.Add(customerObj);
                _customerRepo.Setup(x => x.GetAll()).Returns(listCustomer);
                _customerRepo.Setup(x => x.Delete(It.IsAny<Customer>()));
                SampleSqlController controller = new SampleSqlController(_customerRepo.Object, _unitOfWork.Object, _cache.Object);

                controller.Delete(1);

                Assert.True(true);
            }
            catch (Exception ex)
            {
                Assert.False(true, "Test case failed: " + ex.Message);
            }
        }

        [Fact]
        public void Test_Put_Positive_Response()
        {
            try
            {
                Initialize();
                _customerRepo.Setup(x => x.Update(It.IsAny<Customer>()));
                SampleSqlController controller = new SampleSqlController(_customerRepo.Object, _unitOfWork.Object, _cache.Object);
                controller.Put(new Customer());

                Assert.True(true);
            }
            catch (Exception ex)
            {
                Assert.False(true, "Test case failed: " + ex.Message);
            }
        }

    }
}

using APIpayApplication.Controllers;
using APIpayApplication.Models;
using APIpayApplication.Repository;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestAPIPayApplication.Repository;

namespace TestAPIPayApplication.ControllersTest
{
    public class IncomeControllerTest
    {
        private readonly string idUser = "1";
        private readonly string idIncomeOK = "1";
        private readonly string idIncomeOK2 = "2";
        private readonly string idIncomeOK3 = "3";
        private readonly string idIncomeNotFound = "100";

        IncomeController _controller;
        ICrudRepository<Income> _service;

        public IncomeControllerTest()
        {
            _service = new IncomeServiceFake();
            _controller = new IncomeController(_service);
        }

        //Test Get All
        [Test]
        public void Income_01Test_GetAllOK()
        {
            // Act
            var okResult = _controller.Get(idUser);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(okResult);
        }

        [Test]
        public void Income_02Test_GetAllItems()
        {
            // Act
            var okResult = _controller.Get(idUser) as OkObjectResult;

            // Assert
            Assert.IsInstanceOf<IEnumerable<Income>>(okResult.Value);
            var items = (IEnumerable<Income>)okResult.Value;
            Assert.AreEqual(3, items.Count());
        }

        //Test Get
        [Test]
        public void Income_03Test_GetNotFoundResult()
        {
            // Act
            var notFoundResult = _controller.Get(idIncomeNotFound, idUser);

            // Assert
            Assert.IsInstanceOf<NotFoundResult>(notFoundResult);
        }

        [Test]
        public void Income_04Test_GetOK()
        {
            // Act
            var okResult = _controller.Get(idIncomeOK, idUser);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(okResult);
        }

        [Test]
        public void Income_05Test_GetOKItem()
        {
            // Act
            var okResult = _controller.Get(idIncomeOK, idUser) as OkObjectResult;

            // Assert
            Assert.IsInstanceOf<Income>(okResult.Value);
            Assert.AreEqual(idIncomeOK, (okResult.Value as Income).IdCard);
        }

        //Test Delete
        [Test]
        public void Income_06Test_RemoveNotFound()
        {
            // Act
            var badResponse = _controller.Delete(idIncomeNotFound, idUser);

            // Assert
            Assert.IsInstanceOf<NotFoundResult>(badResponse);
        }

        [Test]
        public void Income_07Test_RemoveOK()
        {
            // Act
            var okResponse = _controller.Delete(idIncomeOK, idUser);

            // Assert
            Assert.IsInstanceOf<OkResult>(okResponse);
        }

        [Test]
        public void Income_08Test_RemoveOKItem()
        {
            // Act
            var okResponse = _controller.Delete(idIncomeOK2, idUser);

            // Assert
            Assert.AreEqual(1, _service.getAll().Count());
        }

        //Test Add
        [Test]
        public void Income_09Test_AddOK()
        {
            // Arrange
            Income testItem = new Income()
            {
                IdIncome = "10",
                IdCard = "1",
                IdUser = idUser,
                Amount = 1200,
                DateCreation = DateTime.Now,
                DateApply = DateTime.Now,
                Description = "nuevo income test"
            };

            // Act
            var createdResponse = _controller.Post(testItem);

            // Assert
            Assert.IsInstanceOf<CreatedAtActionResult>(createdResponse);
        }


        [Test]
        public void Income_10Test_AddOKItem()
        {
            // Arrange
            Income testItem = new Income()
            {
                IdIncome = "20",
                IdCard = "1",
                IdUser = idUser,
                Amount = 1200,
                DateCreation = DateTime.Now,
                DateApply = DateTime.Now,
                Description = "nuevo income test 1"
            };

            // Act
            var createdResponse = _controller.Post(testItem) as CreatedAtActionResult;
            var item = createdResponse.Value as Income;

            // Assert
            Assert.IsInstanceOf<Income>(item);
            Assert.AreEqual("nuevo income test 1", item.Description);
        }

        //Test Update
        [Test]
        public void Income_11Test_UpdateNotFound()
        {
            // Act
            var badResponse = _controller.Put(null);

            // Assert
            Assert.IsInstanceOf<NoContentResult>(badResponse);
        }

        [Test]
        public void Income_12Test_UpdateOK()
        {
            // Arrange
            Income testItem = new Income()
            {
                IdIncome = idIncomeOK3,
                IdCard = "1",
                IdUser = idUser,
                Amount = 1200,
                DateCreation = DateTime.Now,
                DateApply = DateTime.Now,
                Description = "prueba update income"
            };

            // Act
            var updateResponse = _controller.Put(testItem);

            // Assert
            Assert.IsInstanceOf<OkResult>(updateResponse);
        }
        [Test]
        public void Income_13Test_UpdateOKItem()
        {
            // Arrange
            Income testItem = new Income()
            {
                IdIncome = idIncomeOK3,
                IdCard = "1",
                IdUser = idUser,
                Amount = 1200,
                DateCreation = DateTime.Now,
                DateApply = DateTime.Now,
                Description = "prueba update income update"
            };

            // Act
            var updatedResponse = _controller.Put(testItem) as OkResult;
            var okResult = _controller.Get(idIncomeOK3, idUser) as OkObjectResult;

            // Asserts
            Assert.IsInstanceOf<OkResult>(updatedResponse);
            Assert.IsInstanceOf<Income>(okResult.Value);
            Assert.AreEqual("prueba update income update", (okResult.Value as Income).Description);
        }
    }
}

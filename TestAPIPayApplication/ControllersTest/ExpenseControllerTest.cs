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
    public class ExpenseControllerTest
    {
        private readonly string idUser = "1";
        private readonly string idExpenseOK = "1";
        private readonly string idExpenseOK2 = "2";
        private readonly string idExpenseOK3 = "3";
        private readonly string idExpenseNotFound = "100";

        ExpenseController _controller;
        ICrudRepository<Expense> _service;

        public ExpenseControllerTest()
        {
            _service = new ExpenseServiceFake();
            _controller = new ExpenseController(_service);
        }

        //Test Get All
        [Test]
        public void Expense_01Test_GetAllOK()
        {
            // Act
            var okResult = _controller.Get(idUser);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(okResult);
        }

        [Test]
        public void Expense_02Test_GetAllItems()
        {
            // Act
            var okResult = _controller.Get(idUser) as OkObjectResult;

            // Assert
            Assert.IsInstanceOf<IEnumerable<Expense>>(okResult.Value);
            var items = (IEnumerable<Expense>)okResult.Value;
            Assert.AreEqual(3, items.Count());
        }

        //Test Get
        [Test]
        public void Expense_03Test_GetNotFoundResult()
        {
            // Act
            var notFoundResult = _controller.Get(idExpenseNotFound, idUser);

            // Assert
            Assert.IsInstanceOf<NotFoundResult>(notFoundResult);
        }

        [Test]
        public void Expense_04Test_GetOK()
        {
            // Act
            var okResult = _controller.Get(idExpenseOK, idUser);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(okResult);
        }

        [Test]
        public void Expense_05Test_GetOKItem()
        {
            // Act
            var okResult = _controller.Get(idExpenseOK, idUser) as OkObjectResult;

            // Assert
            Assert.IsInstanceOf<Expense>(okResult.Value);
            Assert.AreEqual(idExpenseOK, (okResult.Value as Expense).IdCard);
        }

        //Test Delete
        [Test]
        public void Expense_06Test_RemoveNotFound()
        {
            // Act
            var badResponse = _controller.Delete(idExpenseNotFound, idUser);

            // Assert
            Assert.IsInstanceOf<NotFoundResult>(badResponse);
        }

        [Test]
        public void Expense_07Test_RemoveOK()
        {
            // Act
            var okResponse = _controller.Delete(idExpenseOK, idUser);

            // Assert
            Assert.IsInstanceOf<OkResult>(okResponse);
        }

        [Test]
        public void Expense_08Test_RemoveOKItem()
        {
            // Act
            var okResponse = _controller.Delete(idExpenseOK2, idUser);

            // Assert
            Assert.AreEqual(1, _service.getAll().Count());
        }

        //Test Add
        [Test]
        public void Expense_09Test_AddOK()
        {
            // Arrange
            Expense testItem = new Expense()
            {
                IdExpense = "10",
                IdCard = "1",
                IdUser = idUser,
                Amount = 1200,
                DateCreation = DateTime.Now,
                DateApply = DateTime.Now,
                Description = "nuevo expense test"
            };

            // Act
            var createdResponse = _controller.Post(testItem);

            // Assert
            Assert.IsInstanceOf<CreatedAtActionResult>(createdResponse);
        }


        [Test]
        public void Expense_10Test_AddOKItem()
        {
            // Arrange
            Expense testItem = new Expense()
            {
                IdExpense = "20",
                IdCard = "1",
                IdUser = idUser,
                Amount = 1200,
                DateCreation = DateTime.Now,
                DateApply = DateTime.Now,
                Description = "nuevo expense test 1"
            };

            // Act
            var createdResponse = _controller.Post(testItem) as CreatedAtActionResult;
            var item = createdResponse.Value as Expense;

            // Assert
            Assert.IsInstanceOf<Expense>(item);
            Assert.AreEqual("nuevo expense test 1", item.Description);
        }

        //Test Update
        [Test]
        public void Expense_11Test_UpdateNotFound()
        {
            // Act
            var badResponse = _controller.Put(null);

            // Assert
            Assert.IsInstanceOf<NoContentResult>(badResponse);
        }

        [Test]
        public void Expense_12Test_UpdateOK()
        {
            // Arrange
            Expense testItem = new Expense()
            {
                IdExpense = idExpenseOK3,
                IdCard = "1",
                IdUser = idUser,
                Amount = 1200,
                DateCreation = DateTime.Now,
                DateApply = DateTime.Now,
                Description = "prueba update expense"
            };

            // Act
            var updateResponse = _controller.Put(testItem);

            // Assert
            Assert.IsInstanceOf<OkResult>(updateResponse);
        }
        [Test]
        public void Expense_13Test_UpdateOKItem()
        {
            // Arrange
            Expense testItem = new Expense()
            {
                IdExpense = idExpenseOK3,
                IdCard = "1",
                IdUser = idUser,
                Amount = 1200,
                DateCreation = DateTime.Now,
                DateApply = DateTime.Now,
                Description = "prueba update expense update"
            };

            // Act
            var updatedResponse = _controller.Put(testItem) as OkResult;
            var okResult = _controller.Get(idExpenseOK3, idUser) as OkObjectResult;

            // Asserts
            Assert.IsInstanceOf<OkResult>(updatedResponse);
            Assert.IsInstanceOf<Expense>(okResult.Value);
            Assert.AreEqual("prueba update expense update", (okResult.Value as Expense).Description);
        }
    }
}

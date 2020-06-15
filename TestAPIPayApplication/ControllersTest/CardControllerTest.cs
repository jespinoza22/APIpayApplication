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
    public class CardControllerTest
    {
        private readonly string idUser = "1";
        private readonly string idCardOK = "1";
        private readonly string idCardOK2 = "2";
        private readonly string idCardOK3 = "3";
        private readonly string idCardNotFound = "100";
        CardController _controller;
        ICrudRepository<Card> _service;

        public CardControllerTest() {
            _service = new CardServiceFake();
            _controller = new CardController(_service);
        }


        //Test Get All
        [Test]
        public void Card_01Test_GetAllOK()
        {
            // Act
            var okResult = _controller.Get(string.Empty, idUser, string.Empty);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(okResult);
        }

        [Test]
        public void Card_02Test_GetAllItems()
        {
            // Act
            var okResult = _controller.Get(string.Empty, idUser, string.Empty) as OkObjectResult;

            // Assert
            Assert.IsInstanceOf<IEnumerable<Card>>(okResult.Value);
            var items = (IEnumerable<Card>) okResult.Value;
            Assert.AreEqual(3, items.Count());
        }

        //Test Get
        [Test]
        public void Card_03Test_GetNotFoundResult()
        {
            // Act
            var notFoundResult = _controller.Get(idCardNotFound, idUser, string.Empty);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(notFoundResult);
        }

        [Test]
        public void Card_04Test_GetOK()
        {
            // Act
            var okResult = _controller.Get(idCardOK, idUser, string.Empty);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(okResult);
        }

        [Test]
        public void Card_05Test_GetOKItem()
        {
            // Act
            var okResult = _controller.Get(idCardOK, idUser, string.Empty) as OkObjectResult;

            // Assert
            Assert.IsInstanceOf<List<Card>>(okResult.Value);
            Assert.AreEqual(idCardOK, (okResult.Value as List<Card>)[0].IdCard);
        }

        //Test Delete
        [Test]
        public void Card_06Test_RemoveNotFound()
        {
            // Act
            var badResponse = _controller.Delete(idCardNotFound, idUser);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(badResponse);
        }

        [Test]
        public void Card_07Test_RemoveOK()
        {
            // Act
            var okResponse = _controller.Delete(idCardOK, idUser);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(okResponse);
        }

        [Test]
        public void Card_08Test_RemoveOKItem()
        {
            // Act
            var okResponse = _controller.Delete(idCardOK2, idUser);

            // Assert
            Assert.AreEqual(1, _service.getAll().Count());
        }

        //Test Add
        [Test]
        public void Card_09Test_AddOK()
        {
            // Arrange
            Card testItem = new Card()
            {
                IdCard = "10",
                IdUser = idUser,
                CardNumber = "0",
                DateCreate = DateTime.Now,
                DateModify = DateTime.Now,
                Description = "nueva tarjeta test"
            };

            // Act
            var createdResponse = _controller.Post(testItem);

            // Assert
            Assert.IsInstanceOf<CreatedAtActionResult>(createdResponse);
        }


        [Test]
        public void Card_10Test_AddOKItem()
        {
            // Arrange
            var testItem = new Card()
            {
                IdCard = "20",
                IdUser = idUser,
                CardNumber = "0",
                DateCreate = DateTime.Now,
                DateModify = DateTime.Now,
                Description = "nueva tarjeta test 1"
            };

            // Act
            var createdResponse = _controller.Post(testItem) as CreatedAtActionResult;
            var item = createdResponse.Value as Card;

            // Assert
            Assert.IsInstanceOf<Card>(item);
            Assert.AreEqual("nueva tarjeta test 1", item.Description);
        }

        //Test Update
        [Test]
        public void Card_11Test_UpdateNotFound()
        {
            // Act
            var badResponse = _controller.Put(null);

            // Assert
            Assert.IsInstanceOf<NoContentResult>(badResponse);
        }

        [Test]
        public void Card_12Test_UpdateOK()
        {
            // Arrange
            Card testItem = new Card()
            {
                IdCard = idCardOK3,
                IdUser = idUser,
                CardNumber = "0",
                DateCreate = DateTime.Now,
                DateModify = DateTime.Now,
                Description = "prueba update tarjeta"
            };

            // Act
            var updateResponse = _controller.Put(testItem);

            // Assert
            Assert.IsInstanceOf<OkResult>(updateResponse);
        }
        [Test]
        public void Card_13Test_UpdateOKItem()
        {
            // Arrange
            Card testItem = new Card()
            {
                IdCard = idCardOK3,
                IdUser = idUser,
                CardNumber = "0",
                DateCreate = DateTime.Now,
                DateModify = DateTime.Now,
                Description = "prueba update tarjeta update"
            };

            // Act
            var updatedResponse = _controller.Put(testItem) as OkResult;
            var okResult = _controller.Get(idCardOK3, idUser, string.Empty) as OkObjectResult;

            // Asserts
            Assert.IsInstanceOf<List<Card>>(okResult.Value);
            Assert.AreEqual("prueba update tarjeta update", (okResult.Value as List<Card>)[0].Description);
        }
    }
}

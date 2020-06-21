using APIpayApplication.Controllers;
using APIpayApplication.Models;
using APIpayApplication.Repository;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using TestAPIPayApplication.Repository;

namespace TestAPIPayApplication.ControllersTest
{
    public class ResumeControllerTest
    {
        private readonly string idUser = "1";
        private readonly string idUserNotFound = "999";
        ResumeController _controller;
        ICrudRepository<Income> _serviceIncome;
        ICrudRepository<Expense> _serviceExpense;

        public ResumeControllerTest()
        {
            _serviceIncome = new IncomeServiceFake();
            _serviceExpense = new ExpenseServiceFake();
            _controller = new ResumeController(_serviceIncome, _serviceExpense);
        }


        //Test Get All
        [Test]
        public void Resume_01Test_GetOK()
        {
            // Act
            var okResult = _controller.Get(idUser, 2020);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(okResult);
        }

        //Test Get Not Found
        [Test]
        public void Resume_02Test_GetNotFound()
        {
            // Act
            var result = _controller.Get(idUserNotFound, 2020) as OkObjectResult;

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
            Assert.AreEqual(0, (result.Value as List<Resume>).Count);
        }

        //Test Get Resumen total
        [Test]
        public void Resume_03Test_GetResumeOK()
        {
            // Act
            var okResult = _controller.GetTotal(idUser);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(okResult);
        }

        //Test Get Resumen total
        [Test]
        public void Resume_04Test_GetResumeNotFound()
        {
            // Act
            var result = _controller.GetTotal(idUserNotFound) as OkObjectResult;

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
            Assert.AreEqual(0, (result.Value as ResumeTotal).totalSoles);
            Assert.AreEqual(0, (result.Value as ResumeTotal).totalDolares);
        }
    }
}

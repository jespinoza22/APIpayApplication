using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APIpayApplication.Models;
using APIpayApplication.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APIpayApplication.Controllers
{

    [ApiController]
    [Authorize]
    public class ResumeController : ControllerBase
    {
        private readonly ICrudRepository<Income> _incomeRepository;
        private readonly ICrudRepository<Expense> _expenseRepository;

        public ResumeController(ICrudRepository<Income> incomeRepository, ICrudRepository<Expense> expenseRepository)
        {
            _incomeRepository = incomeRepository;
            _expenseRepository = expenseRepository;
        }

        [HttpGet]
        [Route("api/resume")]
        public IActionResult Get([FromQuery] string idUser, int year)
        {
            var income = new List<Income>();
            var expense = new List<Expense>();

            income = (from a in _incomeRepository.getAll()
                      where a.IdUser == idUser &&
                      a.DateApply.Year == year
                      select a).ToList();

            expense = (from a in _expenseRepository.getAll()
                       where a.IdUser == idUser &&
                       a.DateApply.Year == year
                       select a).ToList();

            var ListResume = new List<Resume>();
            var resume = new Resume();
            if (income.Count == 0 && expense.Count == 0) return new OkObjectResult(ListResume);
            for (int month = 1; month < 13; month++)
            {
                if (month > DateTime.Now.Month) break;

                resume = new Resume();
                resume.month = month;
                resume.year = year;
                resume.incomeSoles = (from a in income
                                     where a.DateApply.Month == month &&
                                     a.IdMoneda == "01" //soles
                                     select a).ToList().Sum(sum => sum.Amount);
                resume.incomeDolares = (from a in income
                                      where a.DateApply.Month == month &&
                                      a.IdMoneda == "02" //dolares
                                      select a).ToList().Sum(sum => sum.Amount);
                resume.expenseSoles = (from a in expense
                                      where a.DateApply.Month == month &&
                                      a.IdMoneda == "01" //soles
                                      select a).ToList().Sum(sum => sum.Amount);
                resume.expenseDolares = (from a in expense
                                         where a.DateApply.Month == month &&
                                         a.IdMoneda == "02" //dolares
                                         select a).ToList().Sum(sum => sum.Amount);
                resume.totalSoles = resume.incomeSoles - resume.expenseSoles;
                resume.totalDolares = resume.incomeDolares - resume.expenseDolares;
                ListResume.Add(resume);
            }
            //if (income == null) income = new List<Income>();
            return new OkObjectResult(ListResume);
        }

        [HttpGet]
        [Route("api/resumetotal")]
        public IActionResult GetTotal([FromQuery] string idUser)
        {
            var resTotal = new ResumeTotal();

            var sumIncomeSoles = (from a in _incomeRepository.getAll()
                                  where a.IdUser == idUser &&
                                  a.IdMoneda == "01"//soles
                                  select a).ToList().Sum(result => result.Amount);

            var sumIncomeDolares = (from a in _incomeRepository.getAll()
                                  where a.IdUser == idUser &&
                                  a.IdMoneda == "02"//dolares
                                  select a).ToList().Sum(result => result.Amount);

            var sumExpenseSoles = (from a in _expenseRepository.getAll()
                                  where a.IdUser == idUser &&
                                  a.IdMoneda == "01"//soles
                                  select a).ToList().Sum(result => result.Amount);

            var sumExpenseDolares = (from a in _expenseRepository.getAll()
                                    where a.IdUser == idUser &&
                                    a.IdMoneda == "02"//dolares
                                    select a).ToList().Sum(result => result.Amount);

            resTotal.totalSoles = sumIncomeSoles - sumExpenseSoles;
            resTotal.totalDolares = sumIncomeDolares - sumExpenseDolares;
            return new OkObjectResult(resTotal);
        }
    }
}

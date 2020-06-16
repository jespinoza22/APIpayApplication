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
    [Route("api/[controller]")]
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
            for (int month = 1; month < 13; month++)
            {
                if (month > DateTime.Now.Month) break;

                resume = new Resume();
                resume.month = month;
                resume.year = year;
                resume.income = (from a in income
                                 where a.DateApply.Month == month
                                 select a).ToList().Sum(sum => sum.Amount);
                resume.expense = (from a in expense
                                  where a.DateApply.Month == month
                                 select a).ToList().Sum(sum => sum.Amount);
                resume.total = resume.income - resume.expense;
                ListResume.Add(resume);
            }
            //if (income == null) income = new List<Income>();
            return new OkObjectResult(ListResume);
        }
    }
}

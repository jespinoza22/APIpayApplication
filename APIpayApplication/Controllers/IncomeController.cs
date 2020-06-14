using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using APIpayApplication.Models;
using APIpayApplication.Repository;
using APIpayApplication.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Design;

namespace APIpayApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class IncomeController : ControllerBase
    {
        private readonly ICrudRepository<Income> _incomeRepository;

        public IncomeController(ICrudRepository<Income> incomeRepository)
        {
            _incomeRepository = incomeRepository;
        }

        // GET: api/<CardController>
        //[HttpGet]
        //public IActionResult Get([FromHeader] string idUser)
        //{
        //    var incomes = from a in _incomeRepository.getAll()
        //                  where a.IdUser == idUser
        //                  select a;

        //    return new OkObjectResult(incomes);
        //}

        // GET api/<CardController>/5
        [HttpGet]
        public IActionResult Get([FromQuery] string id, string idUser, int month, int year, decimal amount, string description)
        {
            description = (description != string.Empty && description != null) ? description.ToUpper() : string.Empty;
            var income = (from a in _incomeRepository.getAll()
                          where a.IdUser == idUser &&
                          (a.IdIncome == id || id == string.Empty || id is null) &&
                          (a.Description.ToUpper().Contains(description) || description == string.Empty) &&
                          (a.DateApply.Year == year || year == 0) &&
                          (a.DateApply.Month == month || month == 0) && 
                          (a.Amount <= amount || amount == 0)
                          select a).ToList();

            //if (income == null) income = new List<Income>();
            return new OkObjectResult(income);
        }

        // POST api/<CardController>
        [HttpPost]
        public IActionResult Post([FromBody] Income income)
        {
            using (var scope = new TransactionScope())
            {
                income.IdIncome = utils.IdGenerated(Constantes.IncomeValue);
                income.DateCreation = DateTime.Now;
                _incomeRepository.Insert(income);
                scope.Complete();
                return CreatedAtAction(nameof(Get), new { id = income.IdCard }, income);
            }
        }

        // PUT api/<CardController>/5
        [HttpPut]
        public IActionResult Put([FromBody] Income income)
        {
            if (income != null)
            {
                using (var scope = new TransactionScope())
                {
                    _incomeRepository.Update(income);
                    scope.Complete();
                    return new OkResult();
                }

            }
            return new NoContentResult();
        }

        // DELETE api/<CardController>/5
        [HttpDelete]
        public IActionResult Delete([FromQuery] string id)
        {
            var income = (from a in _incomeRepository.getAll()
                          where a.IdIncome == id
                          select a).FirstOrDefault();
            if (income == null) return Ok(new { resultado = "1" });
            _incomeRepository.Delete(id);
            return Ok(new { resultado = "0" });
        }
    }
}

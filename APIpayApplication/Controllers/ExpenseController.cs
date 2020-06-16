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

namespace APIpayApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ExpenseController : ControllerBase
    {
        private readonly ICrudRepository<Expense> _expenseRepository;

        public ExpenseController(ICrudRepository<Expense> expenseRepository)
        {
            _expenseRepository = expenseRepository;
        }

        // GET: api/<CardController>
        //[HttpGet]
        //public IActionResult Get(string idUser)
        //{
        //    var expenses = from a in _expenseRepository.getAll()
        //                   where a.IdUser == idUser
        //                   select a;

        //    return new OkObjectResult(expenses);
        //}

        // GET api/<CardController>/5
        [HttpGet]
        public IActionResult Get([FromQuery] string id, string idUser, int month, int year, decimal amount, string description)
        {
            description = (description != string.Empty && description != null) ? description.ToUpper() : string.Empty;
            var expense = (from a in _expenseRepository.getAll()
                          where a.IdUser == idUser &&
                          (a.IdExpense == id || id == string.Empty || id is null) &&
                          (a.Description.ToUpper().Contains(description) || description == string.Empty) &&
                          (a.DateApply.Year == year || year == 0) &&
                          (a.DateApply.Month == month || month == 0) &&
                          (a.Amount <= amount || amount == 0)
                          select a).ToList();
            return new OkObjectResult(expense);
        }

        // POST api/<CardController>
        [HttpPost]
        public IActionResult Post([FromBody] Expense expense)
        {
            using (var scope = new TransactionScope())
            {
                expense.IdExpense = utils.IdGenerated(Constantes.ExpenseValue);
                expense.DateCreation = DateTime.Now;
                _expenseRepository.Insert(expense);
                scope.Complete();
                return CreatedAtAction(nameof(Get), new { id = expense.IdCard }, expense);
            }
        }

        // PUT api/<CardController>/5
        [HttpPut]
        public IActionResult Put([FromBody] Expense expense)
        {
            if (expense != null)
            {
                using (var scope = new TransactionScope())
                {
                    _expenseRepository.Update(expense);
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
            var expense = (from a in _expenseRepository.getAll()
                          where a.IdExpense == id
                          select a).FirstOrDefault();
            if (expense == null) return Ok(new { resultado = "1" });
            _expenseRepository.Delete(id);
            return Ok(new { resultado = "0" });
        }
    }
}

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
    public class ExpenseController : ControllerBase
    {
        private readonly ICrudRepository<Expense> _expenseRepository;

        public ExpenseController(ICrudRepository<Expense> expenseRepository)
        {
            _expenseRepository = expenseRepository;
        }

        // GET: api/<CardController>
        [HttpGet("{idUser}")]
        [Authorize]
        public IActionResult Get(string idUser)
        {
            var expenses = from a in _expenseRepository.getAll()
                          where a.IdUser == idUser
                          select a;

            return new OkObjectResult(expenses);
        }

        // GET api/<CardController>/5
        [HttpGet("{id}/{idUser}")]
        [Authorize]
        public IActionResult Get(string id, string idUser)
        {
            var expense = (from a in _expenseRepository.getAll()
                           where a.IdUser == idUser && a.IdExpense == id
                           select a).FirstOrDefault();
            if (expense == null) return NotFound();
            return new OkObjectResult(expense);
        }

        // POST api/<CardController>
        [HttpPost]
        [Authorize]
        public IActionResult Post([FromBody] Expense expense)
        {
            using (var scope = new TransactionScope())
            {
                expense.IdExpense = utils.IdGenerated(Constantes.ExpenseValue);
                _expenseRepository.Insert(expense);
                scope.Complete();
                return CreatedAtAction(nameof(Get), new { id = expense.IdCard }, expense);
            }
        }

        // PUT api/<CardController>/5
        [HttpPut]
        [Authorize]
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
        [HttpDelete("{id}/{idUser}")]
        [Authorize]
        public IActionResult Delete(string id, string idUser)
        {
            var expense = (from a in _expenseRepository.getAll()
                           where a.IdUser == idUser && a.IdExpense == id
                           select a).FirstOrDefault();
            if (expense == null) return NotFound();
            _expenseRepository.Delete(id);
            return new OkResult();
        }
    }
}

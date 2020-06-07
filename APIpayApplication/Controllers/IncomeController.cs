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
    public class IncomeController : ControllerBase
    {
        private readonly ICrudRepository<Income> _incomeRepository;

        public IncomeController(ICrudRepository<Income> incomeRepository)
        {
            _incomeRepository = incomeRepository;
        }

        // GET: api/<CardController>
        [HttpGet("{idUser}", Name = "Get")]
        [Authorize]
        public IActionResult Get(string idUser)
        {
            var incomes = from a in _incomeRepository.getAll()
                        where a.IdUser == idUser
                        select a;

            return new OkObjectResult(incomes);
        }

        // GET api/<CardController>/5
        [HttpGet("{id}/{idUser}", Name = "Get")]
        [Authorize]
        public IActionResult Get(string id, string idUser)
        {
            var income = (from a in _incomeRepository.getAll()
                          where a.IdUser == idUser && a.IdIncome == id
                          select a).FirstOrDefault();
            if (income == null) return NotFound();
            return new OkObjectResult(income);
        }

        // POST api/<CardController>
        [HttpPost]
        [Authorize]
        public IActionResult Post([FromBody] Income income)
        {
            using (var scope = new TransactionScope())
            {
                income.IdIncome = utils.IdGenerated(Constantes.IncomeValue);
                _incomeRepository.Insert(income);
                scope.Complete();
                return CreatedAtAction(nameof(Get), new { id = income.IdCard }, income);
            }
        }

        // PUT api/<CardController>/5
        [HttpPut]
        [Authorize]
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
        [HttpDelete("{id}/{idUser}")]
        [Authorize]
        public IActionResult Delete(string id, string idUser)
        {
            var income = (from a in _incomeRepository.getAll()
                          where a.IdUser == idUser && a.IdIncome == id
                          select a).FirstOrDefault();
            if (income == null) return NotFound();
            _incomeRepository.Delete(id);
            return new OkResult();
        }
    }
}

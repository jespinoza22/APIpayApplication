using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APIpayApplication.Models;
using APIpayApplication.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Transactions;
using APIpayApplication.Utils;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace APIpayApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CardController : ControllerBase
    {
        private readonly ICrudRepository<Card> _cardRepository;

        public CardController(ICrudRepository<Card> cardRepository)
        {
            _cardRepository = cardRepository;
        }

        // GET: api/<CardController>
        [HttpGet("{idUser}")]
        [Authorize]
        public IActionResult Get(string idUser)
        {
            var cards = from a in _cardRepository.getAll()
                        where a.IdUser == idUser
                        select a;

            return new OkObjectResult(cards);
        }

        // GET api/<CardController>/5
        [HttpGet("{id}/{idUser}")]
        [Authorize]
        public IActionResult Get(string id, string idUser)
        {
            var card = (from a in _cardRepository.getAll()
                        where a.IdUser == idUser && a.IdCard == id
                        select a).FirstOrDefault();
            if (card == null) return NotFound();
            return new OkObjectResult(card);
        }

        // POST api/<CardController>
        [HttpPost]
        [Authorize]
        public IActionResult Post([FromBody] Card card)
        {
            using (var scope = new TransactionScope())
            {
                card.IdCard = utils.IdGenerated(Constantes.CardValue);
                _cardRepository.Insert(card);
                scope.Complete();
                return CreatedAtAction(nameof(Get), new { id = card.IdCard }, card);
            }            
        }

        // PUT api/<CardController>/5
        [HttpPut]
        [Authorize]
        public IActionResult Put([FromBody] Card card)
        {
            if (card != null)
            {
                using (var scope = new TransactionScope())
                {
                    _cardRepository.Update(card);
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
            var card = (from a in _cardRepository.getAll()
                        where a.IdUser == idUser && a.IdCard == id
                        select a).FirstOrDefault();
            if (card == null) return NotFound();
            _cardRepository.Delete(id);
            return new OkResult();
        }
    }
}

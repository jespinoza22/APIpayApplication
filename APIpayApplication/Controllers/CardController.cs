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
    [Authorize]
    public class CardController : ControllerBase
    {
        private readonly ICrudRepository<Card> _cardRepository;

        public CardController(ICrudRepository<Card> cardRepository)
        {
            _cardRepository = cardRepository;
        }

        // GET: api/<CardController>
        //[HttpGet("{idUser}")]
        //public IActionResult Get(string idUser)
        //{
        //    var cards = from a in _cardRepository.getAll()
        //                where a.IdUser == idUser
        //                select a;

        //    return new OkObjectResult(cards);
        //}

        // GET api/<CardController>/5
        [HttpGet]
        public IActionResult Get([FromQuery] string id, string idUser, string description)
        {
            description = (description != string.Empty && description != null) ? description.ToUpper() : string.Empty;
            var card = (from a in _cardRepository.getAll()
                        where a.IdUser == idUser && 
                        (a.IdCard == id || id == string.Empty || id is null) &&
                        (a.Description.ToUpper().Contains(description) || description == string.Empty)
                        select a).ToList();
            //if (card == null) return NotFound();
            return new OkObjectResult(card);
        }

        // POST api/<CardController>
        [HttpPost]
        public IActionResult Post([FromBody] Card card)
        {
            using (var scope = new TransactionScope())
            {
                card.IdCard = utils.IdGenerated(Constantes.CardValue);
                card.DateCreate = DateTime.Now;
                card.DateModify = DateTime.Now;
                _cardRepository.Insert(card);
                scope.Complete();
                return CreatedAtAction(nameof(Get), new { id = card.IdCard }, card);
            }            
        }

        // PUT api/<CardController>/5
        [HttpPut]
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
        [HttpDelete]
        public IActionResult Delete(string id)
        {
            var card = (from a in _cardRepository.getAll()
                        where a.IdCard == id
                        select a).FirstOrDefault();
            if (card == null) return Ok(new { resultado = "1" });
            _cardRepository.Delete(id);
            return Ok(new { resultado = "0" });
        }
    }
}

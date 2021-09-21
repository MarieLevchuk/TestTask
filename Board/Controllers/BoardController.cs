using Board.DAL;
using Board.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Board.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BoardController : Controller
    {
        CardsRepository repository;

        public BoardController()
        {
            repository = new CardsRepository();
        }


        [HttpGet]
        public IEnumerable<Card> Get()
        {
            return repository.GetAll();
        }

        [HttpGet("{id}")]
        public ActionResult<Card> Get(int id)
        {
            Card card = repository.Find(id);            
            if (card == null)
                return NotFound();
            return new ObjectResult(card);
        }

        [HttpPost]
        public ActionResult<Card> Post(Card card)
        {
            if (card == null)
            {
                return BadRequest();
            }

            repository.Add(card);
            return Ok();
        }

        [HttpPut]
        public ActionResult Put(Card card)
        {
            if (card == null)
            {
                return BadRequest();
            }
            if (!repository.GetAll().Any(x => x.Id == card.Id))
            {
                return NotFound();
            }

            repository.Update(card);
            return Ok();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            repository.Remove(id);
            return Ok();
        }
    }
}

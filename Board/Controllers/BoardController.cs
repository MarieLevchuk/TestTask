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
        ICardsRepository _repository;
        readonly string _filePath = "DAL/cards.json";

        public BoardController()
        {
            _repository = new CardsRepository(_filePath);
        }


        [HttpGet]
        public IEnumerable<Card> Get()
        {
            return _repository.GetAll();
        }

        [HttpGet("{id}")]
        public ActionResult<Card> Get(int id)
        {
            Card card = _repository.Find(id);            
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

            _repository.Add(card);
            return Ok();
        }

        [HttpPut]
        public ActionResult Put(Card card)
        {
            if (card == null)
            {
                return BadRequest();
            }
            if (!_repository.GetAll().Any(x => x.Id == card.Id))
            {
                return NotFound();
            }

            _repository.Update(card);
            return Ok();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            _repository.Remove(id);
            return Ok();
        }
    }
}

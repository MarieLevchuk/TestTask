using Board.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Board.DAL
{
    public class CardsRepository : ICardsRepository
    {
        List<Card> _cards;
        JsonHelper _jhelper;
        public CardsRepository(string filePath)
        {
            if (String.IsNullOrEmpty(filePath))
            {
                throw new ArgumentException(nameof(filePath), $"{nameof(filePath)} can not be null or empty");
            }

            _jhelper = new JsonHelper(filePath);
            _cards = _jhelper.Deserialize().ToList();
        }

        public void Add(Card card)
        {
            if (card == null)
            {
                throw new ArgumentNullException(nameof(card));
            }

            _cards.Add(card);
            _jhelper.Serialize(_cards);
        }

        public Card Find(int id)
        {
            UpdateJson();
            Card card = _cards.FirstOrDefault(x => x.Id == id);
            if (card == null)
            {
                throw new ArgumentException(nameof(id));
            }

            return card;
        }

        public IEnumerable<Card> GetAll()
        {
            UpdateJson();

            return _cards;
        }

        public void Remove(int id)
        {
            var index = _cards.FindIndex(x => x.Id == id);
            if (!_cards.Any(x => x.Id == id))
            {
                throw new ArgumentException(nameof(id));
            }

            _cards.RemoveAt(index);
            _jhelper.Serialize(_cards);
        }

        public void Update(Card card)
        {
            if (card == null)
            {
                throw new ArgumentNullException(nameof(card));
            }
            if (!_cards.Any(x => x.Id == card.Id))
            {
                throw new ArgumentException(nameof(card));
            }

            var index = _cards.FindIndex(x => x.Id == card.Id);
            _cards[index] = card;
            _jhelper.Serialize(_cards);
        }
    }
}


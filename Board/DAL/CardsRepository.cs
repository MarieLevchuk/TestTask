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
        List<Card> cards;

        public CardsRepository()
        {
            using (StreamReader streamReader = new StreamReader("DAL/users.json"))
            {
                string json = streamReader.ReadToEnd();
                cards = JsonConvert.DeserializeObject<List<Card>>(json);
            }
        }

        public void Add(Card card)
        {
            if (card == null)
            {
                throw new ArgumentNullException(nameof(card));
            }

            cards.Add(card);
        }

        public Card Find(int id)
        {
            UpdateJson();
            Card card = cards.FirstOrDefault(x => x.Id == id);
            if (card == null)
            {
                throw new ArgumentException(nameof(id));
            }

            return card;
        }

        public IEnumerable<Card> GetAll()
        {
            UpdateJson();

            return cards;
        }

        public void Remove(int id)
        {
            var index = cards.FindIndex(x => x.Id == id);
            if (!cards.Any(x => x.Id == id))
            {
                throw new ArgumentException(nameof(id));
            }

            cards.RemoveAt(index);
        }

        public void Update(Card card)
        {
            if (card == null)
            {
                throw new ArgumentNullException(nameof(card));
            }
            if (!cards.Any(x => x.Id == card.Id))
            {
                throw new ArgumentException(nameof(card));
            }

            var index = cards.FindIndex(x => x.Id == card.Id);
            cards[index] = card;
        }

        private void UpdateJson()
        {
            using (StreamWriter streamWriter = new StreamWriter("DAL/users.json"))
            using (JsonWriter writer = new JsonTextWriter(streamWriter))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(writer, cards);
            }
        }
    }
}


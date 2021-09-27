using Board.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Board.DAL
{
    public interface ICardsRepository
    {
        void Add(Card card);
        IEnumerable<Card> GetAll();
        Card Find(int id);
        void Remove(int id);
        void Update(Card card);
    }
}

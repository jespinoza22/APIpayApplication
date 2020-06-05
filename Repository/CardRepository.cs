using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APIpayApplication.DbContext;
using APIpayApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace APIpayApplication.Repository
{
    public class CardRepository : ICrudRepository<Card>
    {
        private readonly PayContext _dbContext;
        public CardRepository(PayContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Delete(string id)
        {
            var card = _dbContext.Cards.Find(id);
            _dbContext.Cards.Remove(card);
            Save();
        }

        public IEnumerable<Card> getAll()
        {
            return _dbContext.Cards.ToList();
        }

        public Card GetByID(string id)
        {
            return _dbContext.Cards.Find(id);
        }

        public void Insert(Card value)
        {
            _dbContext.Add(value);
            Save();
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }

        public void Update(Card value)
        {
            _dbContext.Entry(value).State = EntityState.Modified;
            Save();
        }
    }
}

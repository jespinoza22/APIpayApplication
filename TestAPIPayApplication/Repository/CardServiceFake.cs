using APIpayApplication.Models;
using APIpayApplication.Repository;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestAPIPayApplication.Repository
{
    public class CardServiceFake : ICrudRepository<Card>
    {

        private readonly List<Card> _card;

        public CardServiceFake() {
            _card = new List<Card>() {
                new Card() { IdCard = "1", IdUser = "1", Description = "Nueva tarjeta 1", CardNumber = "0", DateCreate = DateTime.Now, DateModify = DateTime.Now },
                new Card() { IdCard = "2", IdUser = "1", Description = "Nueva tarjeta 2", CardNumber = "0", DateCreate = DateTime.Now, DateModify = DateTime.Now },
                new Card() { IdCard = "3", IdUser = "1", Description = "Nueva tarjeta 3", CardNumber = "0", DateCreate = DateTime.Now, DateModify = DateTime.Now }
            };
        }

        public void Delete(string id)
        {
            var card = _card.First(a => a.IdCard == id);
            _card.Remove(card);
        }

        public IEnumerable<Card> getAll()
        {
            return _card;
        }

        public Card GetByID(string id)
        {
            return _card.Where(a => a.IdCard == id)
                .FirstOrDefault();
        }

        public void Insert(Card value)
        {
            _card.Add(value);
        }

        public void Save()
        {
            var result = "OK";
        }

        public void Update(Card value)
        {
            var card = _card.FirstOrDefault(x => x.IdCard == value.IdCard);
            if (card != null) {
                card.Description = value.Description;
                card.DateModify = value.DateModify;
                card.CardNumber = value.CardNumber;
            };
        }
    }
}

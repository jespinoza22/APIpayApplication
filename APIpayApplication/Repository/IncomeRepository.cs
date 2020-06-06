using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APIpayApplication.DbContext;
using APIpayApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace APIpayApplication.Repository
{
    public class IncomeRepository : ICrudRepository<Income>
    {
        private readonly PayContext _dbContext;

        public IncomeRepository(PayContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Delete(string id)
        {
            var income = _dbContext.Incomes.Find(id);
            _dbContext.Incomes.Remove(income);
            Save();
        }

        public IEnumerable<Income> getAll()
        {
            return _dbContext.Incomes.ToList();
        }

        public Income GetByID(string id)
        {
            return _dbContext.Incomes.Find(id);
        }

        public void Insert(Income value)
        {
            _dbContext.Add(value);
            Save();
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }

        public void Update(Income value)
        {
            _dbContext.Entry(value).State = EntityState.Modified;
            Save();
        }
    }
}

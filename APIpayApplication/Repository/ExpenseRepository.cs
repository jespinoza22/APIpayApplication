using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APIpayApplication.DbContext;
using APIpayApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace APIpayApplication.Repository
{
    public class ExpenseRepository :ICrudRepository<Expense>
    {
        private readonly PayContext _dbContext;
        public ExpenseRepository(PayContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Delete(string id)
        {
            var expense = _dbContext.Expenses.Find(id);
            _dbContext.Expenses.Remove(expense);
            Save();
        }

        public IEnumerable<Expense> getAll()
        {
            return _dbContext.Expenses.ToList();
        }

        public Expense GetByID(string id)
        {
            return _dbContext.Expenses.Find(id);
        }

        public void Insert(Expense value)
        {
            _dbContext.Add(value);
            Save();
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }

        public void Update(Expense value)
        {
            _dbContext.Entry(value).State = EntityState.Modified;
            Save();
        }
    }
}

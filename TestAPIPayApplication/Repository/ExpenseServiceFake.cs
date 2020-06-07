using APIpayApplication.Models;
using APIpayApplication.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestAPIPayApplication.Repository
{
    public class ExpenseServiceFake : ICrudRepository<Expense>
    {
        private readonly List<Expense> _expense;

        public ExpenseServiceFake() {
            _expense = new List<Expense>() {
                new Expense(){ IdExpense = "1", IdUser = "1", IdCard = "1", Amount = 1500, DateCreation = DateTime.Now, DateApply = DateTime.Now, Description = "nuevo expense 1" },
                new Expense(){ IdExpense = "2", IdUser = "1", IdCard = "1", Amount = 1600, DateCreation = DateTime.Now, DateApply = DateTime.Now, Description = "nuevo expense 2" },
                new Expense(){ IdExpense = "3", IdUser = "1", IdCard = "1", Amount = 1700, DateCreation = DateTime.Now, DateApply = DateTime.Now, Description = "nuevo expense 3" }
            };
        }

        public void Delete(string id)
        {
            var expense = _expense.First(a => a.IdExpense == id);
            _expense.Remove(expense);
        }

        public IEnumerable<Expense> getAll()
        {
            return _expense;
        }

        public Expense GetByID(string id)
        {
            return _expense.Where(a => a.IdExpense == id)
                .FirstOrDefault();
        }

        public void Insert(Expense value)
        {
            _expense.Add(value);
        }

        public void Save()
        {
            var result = "OK";
        }

        public void Update(Expense value)
        {
            var expense = _expense.FirstOrDefault(x => x.IdExpense == value.IdExpense);
            if (expense != null)
            {
                expense.Description = value.Description;
                expense.IdExpense = value.IdExpense;
                expense.DateApply = value.DateApply;
                expense.Amount = value.Amount;
            };
        }
    }
}

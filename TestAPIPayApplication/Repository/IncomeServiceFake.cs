using APIpayApplication.Models;
using APIpayApplication.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestAPIPayApplication.Repository
{
    public class IncomeServiceFake : ICrudRepository<Income>
    {
        private readonly List<Income> _income;

        public IncomeServiceFake() {
            _income = new List<Income>() { 
                new Income(){ IdIncome = "1", IdUser ="1", IdCard ="1", Amount=100, DateCreation=DateTime.Now, DateApply=DateTime.Now, Description = "nuevo income 1" },
                new Income(){ IdIncome = "2", IdUser ="1", IdCard ="1", Amount=100, DateCreation=DateTime.Now, DateApply=DateTime.Now, Description = "nuevo income 2" },
                new Income(){ IdIncome = "3", IdUser ="1", IdCard ="1", Amount=100, DateCreation=DateTime.Now, DateApply=DateTime.Now, Description = "nuevo income 3" }
            };
        }

        public void Delete(string id)
        {
            var income = _income.First(a => a.IdIncome == id);
            _income.Remove(income);
        }

        public IEnumerable<Income> getAll()
        {
            return _income;
        }

        public Income GetByID(string id)
        {
            return _income.Where(a => a.IdIncome == id)
                .FirstOrDefault();
        }

        public void Insert(Income value)
        {
            _income.Add(value);
        }

        public void Save()
        {
            var result = "OK";
        }

        public void Update(Income value)
        {
            var income = _income.FirstOrDefault(x => x.IdIncome == value.IdIncome);
            if (income != null)
            {
                income.Description = value.Description;
                income.IdIncome = value.IdIncome;
                income.DateApply = value.DateApply;
            };
        }
    }
}

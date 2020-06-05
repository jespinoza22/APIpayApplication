using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APIpayApplication.DbContext;

namespace APIpayApplication.Repository
{
    public class IncomeRepository : ICrudRepository<IncomeRepository>
    {
        private readonly PayContext _dbContext;

        public IncomeRepository(PayContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Delete(string id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IncomeRepository> getAll()
        {
            throw new NotImplementedException();
        }

        public IncomeRepository GetByID(string id)
        {
            throw new NotImplementedException();
        }

        public void Insert(IncomeRepository value)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        public void Update(IncomeRepository value)
        {
            throw new NotImplementedException();
        }
    }
}

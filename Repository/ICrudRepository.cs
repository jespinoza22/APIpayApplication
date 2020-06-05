using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIpayApplication.Repository
{
    public interface ICrudRepository<T>
    {
        IEnumerable<T> getAll();
        T GetByID(string id);
        void Insert(T value);
        void Delete(string id);
        void Update(T value);
        void Save();
    }
}

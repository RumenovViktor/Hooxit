using System.Collections.Generic;
using System.Linq;

namespace Hooxit.Data.Contracts
{
    public interface IRepository<T>
    {
        IList<T> All();
        T Get(int id);
        IQueryable GetMany(int[] id);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}

using System.Collections.Generic;

namespace Hooxit.Data.Contracts
{
    public interface IRepository<T>
    {
        void Add(T entity);
        void Save();
    }
}

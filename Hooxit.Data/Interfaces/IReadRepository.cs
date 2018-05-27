using System.Collections.Generic;

namespace Hooxit.Data.Contracts
{
    public interface IReadRepository<T>
    {
        IList<T> GetAll();
        T GetById(int id);
        IList<T> GetManyByIds(int[] ids);
    }
}

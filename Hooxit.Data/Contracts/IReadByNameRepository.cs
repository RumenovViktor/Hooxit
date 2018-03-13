using System.Collections.Generic;

namespace Hooxit.Data.Contracts
{
    public interface IReadByNameRepository<T>
    {
        IEnumerable<T> GetByName(string name);
    }
}

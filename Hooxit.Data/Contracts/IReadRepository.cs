using System;
using System.Collections.Generic;
using System.Text;

namespace Hooxit.Data.Contracts
{
    public interface IReadRepository<T>
    {
        IList<T> GetAll();
        T GetById(int id);
    }
}

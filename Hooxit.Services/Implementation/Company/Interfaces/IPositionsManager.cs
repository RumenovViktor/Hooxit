using Hooxit.Presentation;
using System.Collections.Generic;

namespace Hooxit.Services.Implementation.Company.Interfaces
{
    public interface IPositionsManager
    {
        IEnumerable<IdNameReadModel> GetAll(int id);
    }
}

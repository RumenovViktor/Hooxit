using System.Collections.Generic;

using Hooxit.Presentation.Implemenation;
using Hooxit.Presentation.Implemenation.Company.Read;

namespace Hooxit.Services.Company.Interfaces
{
    public interface IPositionsManager
    {
        IEnumerable<IdNameReadModel> GetAll(int id);

        PositionReadModel GetPosition(int companyId, int positionId);
    }
}

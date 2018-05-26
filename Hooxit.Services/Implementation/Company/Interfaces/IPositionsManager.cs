using Hooxit.Presentation;
using Hooxit.Presentation.Company.Read;
using System.Collections.Generic;

namespace Hooxit.Services.Company.Interfaces
{
    public interface IPositionsManager
    {
        IEnumerable<IdNameReadModel> GetAll(int id);

        PositionReadModel GetPosition(int companyId, int positionId);
    }
}

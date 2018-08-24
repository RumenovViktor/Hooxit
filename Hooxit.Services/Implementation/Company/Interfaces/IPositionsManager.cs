using System.Collections.Generic;

using Hooxit.Presentation.Implemenation;
using Hooxit.Presentation.Implemenation.Company.Read;
using Hooxit.Presentation.Implementation.Company.Read;
using Hooxit.Presentation.Implementation.Read.Company;

namespace Hooxit.Services.Company.Interfaces
{
    public interface IPositionsManager
    {
        IEnumerable<PositionStatisticsReadModel> GetAll(int id);

        PositionReadModel GetPosition(int companyId, int positionId);

        PositionReadModel GetPositionById(int positionId);

        IEnumerable<IdNameReadModel> GetPositionsBasicData(int id);

        IEnumerable<AppliedCandidate> GetAppliedCandidates(int positionId);
    }
}

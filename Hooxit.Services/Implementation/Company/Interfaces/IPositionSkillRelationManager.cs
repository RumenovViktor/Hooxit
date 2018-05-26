using Hooxit.Presentation.Company.Read;
using System.Collections.Generic;

namespace Hooxit.Services.Company.Interfaces
{
    public interface IPositionSkillRelationManager
    {
        RelationRead<PositionSkillsRelation> GetRelation(string candidateName, int positionId);
    }
}

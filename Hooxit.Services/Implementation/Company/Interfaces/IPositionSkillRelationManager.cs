using Hooxit.Presentation.Implemenation;

namespace Hooxit.Services.Company.Interfaces
{
    public interface IPositionSkillRelationManager
    {
        RelationRead<PositionSkillsRelation> GetRelation(string candidateName, int positionId);
    }
}

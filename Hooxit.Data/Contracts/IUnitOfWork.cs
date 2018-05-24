using Hooxit.Data.Repository;
using Hooxit.Models;

namespace Hooxit.Data.Contracts
{
    public interface IUnitOfWork
    {
        IRepository<Experience> BuildExperienceRepository();
        IReadRepository<Country> BuildCountriesRepository();
        ICandidateRepository BuildCandidateRepository();
        ICompaniesRepository BuildCompaniesRepository();
        IReadByNameRepository<Skill> BuildSkillsReadByNameRepository();
        IReadRepository<Skill> BuildSkillsReadRepository();
        IReadRepository<PositionSkill> BuildPositionSkillRepository();
        IRepository<Position> BuildPositionsRepository();
        IRepository<CandidateSkill> BuildCandidateSkillRepository();
    }
}

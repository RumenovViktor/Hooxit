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
        IReadRepository<Position> BuildPositionsReadRepository();
        IRepository<CandidateSkill> BuildCandidateSkillRepository();
        IReadRepository<CandidateSkill> BuildCandidateSkillReadRepository();
        IReadRepository<Experience> BuildExperienceReadRepository();
        IDeleteRepository<CandidateSkill> BuildCandidateSkillDeleteRepository();
        IUpdateRepository<Position> BuildPositionsUpdateRepository();
        IReadRepository<Product> BuildProductsRepository();
        IRepository<Product> BuildProductsCreateRepository();        
        IUpdateRepository<Product> BuildProductsUpdateRepository();
        IRepository<PositionCandidate> BuildPositionCandidateRepository();
        IReadRepository<PositionCandidate> BuildPositionCandidateReadRepository();
        IRepository<CandidateInterest> BuildCandidateInterestRepository();
        IReadRepository<CandidateInterest> BuildCandidateInterestReadRepository();
        IDeleteRepository<CandidateInterest> BuildCandidateInterestDeleteRepository();
    }
}

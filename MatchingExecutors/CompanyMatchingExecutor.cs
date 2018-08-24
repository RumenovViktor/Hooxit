namespace MatchingExecutors
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Hooxit.Data.Contracts;
    using Hooxit.Data.Repository;
    using Hooxit.Models;
    using Hooxit.Models.Users;
    using Hooxit.Presentation.Implemenation.Company.Read.Matching;
    using MatchingExecutors.Base;

    public class CompanyMatchingExecutor : MatchingExecutor<PositionForMatch, SuggestedCandidate>
    {
        private readonly IReadRepository<PositionSkill> positionSkillRepository;
        private readonly IReadRepository<CandidateSkill> candidateSkillRepository;
        private readonly ICandidateRepository candidateRepository;
        private readonly IUserRepository userRepository;

        public CompanyMatchingExecutor(IUnitOfWork unitOfWork, IUserRepository userRepository) : base()
        {
            positionSkillRepository = unitOfWork.BuildPositionSkillRepository();
            candidateSkillRepository = unitOfWork.BuildCandidateSkillReadRepository();
            candidateRepository = unitOfWork.BuildCandidateRepository();

            this.userRepository = userRepository;
        }

        public override IEnumerable<SuggestedCandidate> Match(PositionForMatch matchRequest)
        {
            var suggestedCandidates = new List<SuggestedCandidate>();
            var candidates = candidateRepository.GetAll().ToList();

            foreach (var candidate in candidates)
            {
                var matched = MatchSingle(candidate, matchRequest.PositionId);
                suggestedCandidates.Add(matched);
            }

            return suggestedCandidates.OrderByDescending(x => x.MatchedPercentage);
        }

        public SuggestedCandidate MatchSingle(Candidate candidate, int positionId)
        {
            var user = this.userRepository.Get(candidate.UserId);
            var positionRequiredSkills = GetPositionRequiredSkills(positionId);
            var candidateSkills = GetCandidateSkills(candidate.Id);
            var skillRate = CalculateSkillRate(positionRequiredSkills, candidateSkills);

            var skillMatchInPercentage = positionRequiredSkills.Any()
                ?
                ((decimal)skillRate / positionRequiredSkills.ToList().Count) * TotalPercentage
                :
                TotalPercentage;

            return new SuggestedCandidate
            {
                FullName = candidate.FirstName + " " + candidate.LastName,
                MatchedPercentage = skillMatchInPercentage,
                FormatedMatchedPercentage = string.Format("{0:0.00}", skillMatchInPercentage),
                UserName = user.Result.Username,
                Id = candidate.Id
            };
        }

        private IEnumerable<PositionSkill> GetPositionRequiredSkills(int positionId)
        {
            return positionSkillRepository.GetManyByIds(new int[] { positionId });
        }

        private IEnumerable<CandidateSkill> GetCandidateSkills(int candidateId)
        {
            return candidateSkillRepository.GetManyByIds(new int[] { candidateId });
        }
    }
}

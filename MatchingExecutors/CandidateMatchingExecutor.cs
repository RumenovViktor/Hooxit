using System.Linq;
using System.Collections.Generic;

using Hooxit.Data.Contracts;
using Hooxit.Data.Repository;
using Hooxit.Models;
using Hooxit.Presentation.Implementation.Candidate.Matching;
using MatchingExecutors.Base;

namespace MatchingExecutors
{
    public class CandidateMatchingExecutor : MatchingExecutor<CandidateForMatch, SuggestedPosition>
    {
        private readonly IUserRepository userRepository;
        private readonly IReadRepository<Position> positionsRepository;
        private readonly ICandidateRepository candidateRepository;
        private readonly IReadRepository<PositionSkill> positionSkillReadRepository;
        private readonly IReadRepository<CandidateSkill> candidateSkillReadRepository;

        public CandidateMatchingExecutor(IUserRepository userRepository, IUnitOfWork unitOfWork) : base()
        {
            this.userRepository = userRepository;

            candidateRepository = unitOfWork.BuildCandidateRepository();
            positionSkillReadRepository = unitOfWork.BuildPositionSkillRepository();
            positionsRepository = unitOfWork.BuildPositionsReadRepository();
            candidateSkillReadRepository = unitOfWork.BuildCandidateSkillReadRepository();
        }

        public override IEnumerable<SuggestedPosition> Match(CandidateForMatch matchRequest)
        {
            var user = userRepository.GetByName(matchRequest.Username);
            var candidate = candidateRepository.GetById(user.Result.Id);
            var candidateSkills = candidateSkillReadRepository.GetManyByIds(new[] { candidate.Id });
            var positions = positionsRepository.GetAll().ToList();
            var suggestedPositions = new List<SuggestedPosition>();

            positions.ForEach(x =>
            {
                var positionRequiredSkills = positionSkillReadRepository.GetManyByIds(new int[] { x.PositionID });
                var skillRate = CalculateSkillRate(positionRequiredSkills, candidateSkills);

                var skillMatchInPercentage = positionRequiredSkills.Any() 
                ?
                ((decimal)skillRate / positionRequiredSkills.Count) * TotalPercentage 
                :
                TotalPercentage;

                suggestedPositions.Add(new SuggestedPosition
                {
                    Id = x.PositionID,
                    Name = x.PositionName,
                    MatchedPercentage = skillMatchInPercentage,
                    FormatedMatchedPercentage = string.Format("{0:0.00}", skillMatchInPercentage)
                });
            });

            return suggestedPositions.OrderByDescending(x => x.MatchedPercentage);
        }
    }
}

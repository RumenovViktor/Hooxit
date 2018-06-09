using System.Linq;
using System.Collections.Generic;

using Hooxit.Models;

namespace MatchingExecutors.Base
{
    public abstract class MatchingExecutor<TMatchRequest, TMatchResult>
        where TMatchRequest : class, new()
        where TMatchResult : class, new()
    {
        protected const byte Avarage = 2;
        protected const decimal TotalPercentage = 100;

        public abstract IEnumerable<TMatchResult> Match(TMatchRequest matchRequest);

        // TODO: Make it generic
        protected virtual int CalculateSkillRate(IEnumerable<PositionSkill> requiredSkills, IEnumerable<CandidateSkill> candidateSkills)
        {
            var skillsMatchCount = 0;

            if (!requiredSkills.Any())
            {
                return (int)TotalPercentage;
            }

            foreach (var skill in requiredSkills)
            {
                var userSkill = candidateSkills.Where(x => x.SkillId == skill.SkillId).FirstOrDefault();

                if (userSkill != null)
                {
                    skillsMatchCount++;
                }
            }

            return skillsMatchCount;
        }
    }
}

﻿namespace MatchingExecutors
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Hooxit.Data.Contracts;
    using Hooxit.Data.Repository;
    using Hooxit.Models;
    using Hooxit.Presentation.Implemenation.Company.Read.Matching;
    using MatchingExecutors.Base;

    public class CompanyMatchingExecutor : MatchingExecutor<PositionForMatch, SuggestedCandidate>
    {
        private readonly IReadRepository<PositionSkill> positionSkillRepository;
        private readonly IRepository<CandidateSkill> candidateSkillRepository;
        private readonly ICandidateRepository candidateRepository;
        private readonly IUserRepository userRepository;

        public CompanyMatchingExecutor(IUnitOfWork unitOfWork, IUserRepository userRepository) : base()
        {
            positionSkillRepository = unitOfWork.BuildPositionSkillRepository();
            candidateSkillRepository = unitOfWork.BuildCandidateSkillRepository();
            candidateRepository = unitOfWork.BuildCandidateRepository();

            this.userRepository = userRepository;
        }

        public override IEnumerable<SuggestedCandidate> Match(PositionForMatch matchRequest)
        {
            var suggestedCandidates = new List<SuggestedCandidate>();
            var candidates = this.candidateRepository.GetAll().ToList();

            foreach (var candidate in candidates)
            {
                var user = this.userRepository.Get(candidate.UserId);
                var positionRequiredSkills = GetPositionRequiredSkills(matchRequest.PositionId);
                var candidateSkills = GetCandidateSkills(candidate.Id);
                var skillRate = CalculateSkillRate(positionRequiredSkills, candidateSkills);

                var skillMatchInPercentage = positionRequiredSkills.Any() 
                    ?
                    ((decimal)skillRate / positionRequiredSkills.ToList().Count) * TotalPercentage 
                    : 
                    TotalPercentage;

                suggestedCandidates.Add(new SuggestedCandidate
                {
                    FullName = candidate.FirstName + " " + candidate.LastName,
                    MatchedPercentage = String.Format("{0:0.00}", skillMatchInPercentage),
                    UserName = user.Result.Username,
                    Id = candidate.Id
                });
            }

            return suggestedCandidates.OrderByDescending(x => x.MatchedPercentage);
        }

        private IEnumerable<PositionSkill> GetPositionRequiredSkills(int positionId)
        {
            return positionSkillRepository.GetManyById(new int[] { positionId });
        }

        private IEnumerable<CandidateSkill> GetCandidateSkills(int candidateId)
        {
            return candidateSkillRepository.GetMany(new int[] { candidateId });
        }

        private int CalculateSkillRate(IEnumerable<PositionSkill> requiredSkills, IEnumerable<CandidateSkill> candidateSkills)
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

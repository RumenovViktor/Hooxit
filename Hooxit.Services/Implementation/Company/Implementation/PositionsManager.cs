using System.Linq;
using System.Collections.Generic;

using Hooxit.Data.Contracts;
using Hooxit.Models;
using Hooxit.Presentation.Implemenation;
using Hooxit.Services.Company.Interfaces;
using Hooxit.Presentation.Implemenation.Company.Read;
using Hooxit.Presentation.Implementation.Company.Read;
using Hooxit.Data.Repository;
using MatchingExecutors;
using Hooxit.Presentation.Implementation.Read.Company;

// TODO: Get from company info when Lazy loading is available.
namespace Hooxit.Services.Company.Implemenation
{
    public class PositionsManager : IPositionsManager
    {
        private readonly IReadRepository<Position> positionsReadRepository;
        private readonly IReadRepository<PositionSkill> positionSkillsReadRepository;
        private readonly IReadRepository<Skill> skillsReadRepository;
        private readonly IReadRepository<PositionCandidate> positionsCandidatesRepository;
        private readonly ICandidateRepository candidatesRepository;
        private readonly IUserRepository userRepository;
        private readonly IPositionSkillRelationManager positionSkillRelationManager;
        private readonly CompanyMatchingExecutor matchingExecutor;

        public PositionsManager(IUnitOfWork unitOfWork, IUserRepository userRepository, IPositionSkillRelationManager positionSkillRelationManager)
        {
            positionsReadRepository = unitOfWork.BuildPositionsReadRepository();
            positionSkillsReadRepository = unitOfWork.BuildPositionSkillRepository();
            skillsReadRepository = unitOfWork.BuildSkillsReadRepository();
            positionsCandidatesRepository = unitOfWork.BuildPositionCandidateReadRepository();
            candidatesRepository = unitOfWork.BuildCandidateRepository();

            this.userRepository = userRepository;
            this.positionSkillRelationManager = positionSkillRelationManager;
            this.matchingExecutor = new CompanyMatchingExecutor(unitOfWork, userRepository);
        }

        public IEnumerable<IdNameReadModel> GetPositionsBasicData(int id)
        {
            var allPositions = this.positionsReadRepository.GetAll();

            return allPositions.Where(x => x.CompanyID == id).Select(x => new IdNameReadModel { Id = x.PositionID, Name = x.PositionName }).ToList();
        }

        public IEnumerable<PositionStatisticsReadModel> GetAll(int id)
        {
            var allPositions = this.positionsReadRepository.GetAll().Where(x => x.CompanyID == id).Select(x => new PositionStatisticsReadModel
            {
                IdName = new IdNameReadModel
                {
                    Id = x.PositionID,
                    Name = x.PositionName
                }
            }).ToList();

            allPositions.ForEach((position) => 
            {
                var appliedForPositionCount = positionsCandidatesRepository.GetManyByIds(new[] { position.IdName.Id }).Count();

                position.AppliedCount = appliedForPositionCount;
            });

            return allPositions;
        }

        public PositionReadModel GetPosition(int companyId, int positionId)
        {
            var requiredSkillsIds = this.positionSkillsReadRepository.GetManyByIds(new[] { positionId }).Select(x => x.SkillId);
            var requiredSkills = this.skillsReadRepository.GetManyByIds(requiredSkillsIds.ToArray());
            var position = this.positionsReadRepository.GetAll().Where(x => x.CompanyID == companyId && x.PositionID == positionId).Select(x => this.BuildPositionModel(x, requiredSkills)).FirstOrDefault();

            return position;
        }

        public PositionReadModel GetPositionById(int positionId)
        {
            var companyId = this.positionsReadRepository.GetById(positionId).CompanyID;

            return GetPosition(companyId, positionId);
        }

        public IEnumerable<AppliedCandidate> GetAppliedCandidates(int positionId)
        {
            var candidatesIds = positionsCandidatesRepository.GetManyByIds(new[] { positionId }).Select(x => x.CandidateId);
            var candidates = candidatesRepository.GetManyByIds(candidatesIds.ToArray());

            var appliedCandidates = candidates.Select(x => 
            {
                var idName = new IdNameReadModel { Name = userRepository.Get(x.UserId).Result.UserName };
                var match = this.matchingExecutor.MatchSingle(x, positionId);

                return new AppliedCandidate
                {
                    IdName = idName,
                    MatchPercentage = match.FormatedMatchedPercentage
                };
            });

            return appliedCandidates;
        }

        private PositionReadModel BuildPositionModel(Position position, IList<Skill> requiredSkills)
        {
            return new PositionReadModel
            {
                PositionId = position.PositionID,
                Description = position.Description,
                LookingFor = position.LookingFor,
                MinimumYearsOfExperience = position.MinimumYearsOfExperience,
                PositionName = position.PositionName,
                RequiredSkills = requiredSkills.Select(o => new IdNameReadModel { Id = o.ID, Name = o.SkillName }).ToList(),
                Responsibilities = position.Responsibilities,
                WhatWeOffer = position.WhatWeOffer
            };
        }
    }
}

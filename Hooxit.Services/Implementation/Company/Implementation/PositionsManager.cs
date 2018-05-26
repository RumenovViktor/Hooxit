using Hooxit.Data.Contracts;
using Hooxit.Models;
using Hooxit.Presentation;
using Hooxit.Presentation.Company.Read;
using Hooxit.Services.Company.Interfaces;
using System.Collections.Generic;
using System.Linq;

// TODO: Get from company info when Lazy loading is available.
namespace Hooxit.Services.Company.Implemenation
{
    public class PositionsManager : IPositionsManager
    {
        private readonly IRepository<Position> positionsRepository;
        private readonly IReadRepository<PositionSkill> positionSkillsReadRepository;
        private readonly IReadRepository<Skill> skillsReadRepository;

        public PositionsManager(IUnitOfWork unitOfWork)
        {
            this.positionsRepository = unitOfWork.BuildPositionsRepository();
            this.positionSkillsReadRepository = unitOfWork.BuildPositionSkillRepository();
            this.skillsReadRepository = unitOfWork.BuildSkillsReadRepository();
        }

        public IEnumerable<IdNameReadModel> GetAll(int id)
        {
            var allPositions = this.positionsRepository.All();

            return allPositions.Where(x => x.CompanyID == id).Select(x => new IdNameReadModel
            {
                Id = x.PositionID,
                Name = x.PositionName
            }).ToList();
        }

        public PositionReadModel GetPosition(int companyId, int positionId)
        {
            var requiredSkillsIds = this.positionSkillsReadRepository.GetManyById(new[] { positionId }).Select(x => x.SkillId);
            var requiredSkills = this.skillsReadRepository.GetManyById(requiredSkillsIds.ToArray());
            var position = this.positionsRepository.All().Where(x => x.CompanyID == companyId && x.PositionID == positionId).Select(x => this.BuildPositionModel(x, requiredSkills)).FirstOrDefault();

            return position;
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

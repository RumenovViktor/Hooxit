using Hooxit.Data.Contracts;
using Hooxit.Models;
using Hooxit.Presentation;
using Hooxit.Presentation.Company.Read;
using Hooxit.Services.Implementation.Company.Interfaces;
using System.Collections.Generic;
using System.Linq;

// TODO: Get from company info when Lazy loading is available.
namespace Hooxit.Services.Implementation.Company.Implemenation
{
    public class PositionsManager : IPositionsManager
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IRepository<Position> positionsRepository;

        public PositionsManager(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;

            this.positionsRepository = this.unitOfWork.BuildPositionsRepository();
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
            var position = this.positionsRepository.All().Where(x => x.CompanyID == companyId && x.PositionID == positionId).Select(x => this.BuildPositionModel(x)).FirstOrDefault();
            return position;
        }

        private PositionReadModel BuildPositionModel(Position position)
        {
            return new PositionReadModel
            {
                Description = position.Description,
                LookingFor = position.LookingFor,
                MinimumYearsOfExperience = position.MinimumYearsOfExperience,
                PositionName = position.PositionName,
                RequiredSkills = position.PositionSkill.Select(o => new IdNameReadModel { Id = o.Skill.ID, Name = o.Skill.SkillName }).ToList(),
                Responsibilities = position.Responsibilities,
                WhatWeOffer = position.WhatWeOffer
            };
        }
    }
}

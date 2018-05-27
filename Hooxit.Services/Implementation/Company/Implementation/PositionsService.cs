using System;
using System.Linq;
using System.Threading.Tasks;

using Newtonsoft.Json;
using Microsoft.Extensions.Logging;

using Hooxit.Data.Contracts;
using Hooxit.Data.Repository;
using Hooxit.Models;
using Hooxit.Presentation.Interfaces.Company;
using Hooxit.Services.Company.Interfaces;
using Hooxit.Presentation.Implemenation.Write.Company;
using Hooxit.Presentation.Implemenation.Company.Write;

namespace Hooxit.Services.Company.Implemenation
{
    // TODO: Refactor:
    // 1. Create the positions without second database insert/update
    // 2. Apply Lazy Loading after migration to newest .net core (Remove dependacies from different repositories)
    // 3. User Automapper for mapping 
    public class PositionsService : IPositionsService
    {
        private readonly IRepository<Position> positionsRepository;
        private readonly ICompaniesRepository companiesRepository;
        private readonly IUserRepository userRepository;
        private readonly ILogger logger;

        public PositionsService(IUserRepository userRepository, IUnitOfWork unitOfWork, ILoggerFactory loggerFactory)
        {
            this.companiesRepository = unitOfWork.BuildCompaniesRepository();
            this.positionsRepository = unitOfWork.BuildPositionsRepository();
            this.userRepository = userRepository;
            this.logger = loggerFactory.CreateLogger<PositionsService>();
        }

        public async Task<bool> CreatePosition(CreatePosition createPosition)
        {
            var loggedUser = await this.userRepository.GetByName(UserInfo.UserName);
            var company = this.companiesRepository.GetBydId(loggedUser.Id);

            try
            {
                var position = this.BuildPositionModel(createPosition);
                company.Positions.Add(position);

                await userRepository.Update(loggedUser);

                this.companiesRepository.Save();

                var addedPosition = this.positionsRepository.Get(position.PositionID);

                addedPosition.PositionSkill = createPosition.RequiredSkills.Select(x => new PositionSkill()
                {
                    PositionId = addedPosition.PositionID, SkillId = x
                }).ToList();

                this.companiesRepository.Save();

                return true;
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.InnerException, JsonConvert.SerializeObject(createPosition));

                return false;
            }
        }

        public bool ChangeDescription(IPresentationSegment presentationSegment)
        {
            try
            {
                var position = this.positionsRepository.Get(presentationSegment.Id);
                position.Description = presentationSegment.Content;

                this.positionsRepository.Update(position);
                this.companiesRepository.Save();

                return true;
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.InnerException, JsonConvert.SerializeObject(presentationSegment));
                throw ex;
            }
        }

        public bool ChangeLookingForDescription(IPresentationSegment presentationSegment)
        {
            try
            {
                var position = this.positionsRepository.Get(presentationSegment.Id);
                position.LookingFor = presentationSegment.Content;

                this.positionsRepository.Update(position);
                this.companiesRepository.Save();

                return true;
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.InnerException, JsonConvert.SerializeObject(presentationSegment));
                throw ex;
            }
        }

        public bool ChangeWhatWeOfferDescription(IPresentationSegment presentationSegment)
        {
            try
            {
                var position = this.positionsRepository.Get(presentationSegment.Id);
                position.WhatWeOffer = presentationSegment.Content;

                this.positionsRepository.Update(position);
                this.companiesRepository.Save();

                return true;
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.InnerException, JsonConvert.SerializeObject(presentationSegment));
                throw ex;
            }
        }

        public bool ChangeResponsibilitiesDescription(IPresentationSegment presentationSegment)
        {
            try
            {
                var position = this.positionsRepository.Get(presentationSegment.Id);
                position.Responsibilities = presentationSegment.Content;

                this.positionsRepository.Update(position);
                this.companiesRepository.Save();

                return true;
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.InnerException, JsonConvert.SerializeObject(presentationSegment));
                throw ex;
            }
        }

        public bool ChangeSkills(ChangeSkills changePositionRequiredSkills)
        {
            try
            {
                var position = this.positionsRepository.Get(changePositionRequiredSkills.PositionId);

                position.PositionSkill = changePositionRequiredSkills.RequiredSkills.Select(x => new PositionSkill()
                {
                    PositionId = changePositionRequiredSkills.PositionId,
                    SkillId = x
                }).ToList();

                this.positionsRepository.Update(position);
                this.companiesRepository.Save();

                return true;
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.InnerException, JsonConvert.SerializeObject(changePositionRequiredSkills));
                throw ex;
            }
        }

        public bool ChangePositionName(ChangePositionName changePositionName)
        {
            try
            {
                var position = this.positionsRepository.Get(changePositionName.PositionId);
                position.PositionName = changePositionName.PositionName;

                this.positionsRepository.Update(position);
                this.companiesRepository.Save();

                return true;
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.InnerException, JsonConvert.SerializeObject(changePositionName));
                throw ex;
            }
        }

        private Position BuildPositionModel(CreatePosition createPosition)
        {
            return new Position
            {
                Description = createPosition.Description,
                LookingFor = createPosition.LookingFor,
                MinimumYearsOfExperience = createPosition.MinimumYearsOfExperience,
                Responsibilities = createPosition.Responsibilities,
                WhatWeOffer = createPosition.WhatWeOffer,
                PositionName = createPosition.PositionName
            };
        }
    }
}

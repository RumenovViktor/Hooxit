﻿using Hooxit.Services.Contracts;
using System;
using Hooxit.Presentation.Write;
using System.Threading.Tasks;
using Hooxit.Data.Contracts;
using Hooxit.Models;
using System.Linq;
using Hooxit.Presentation;
using Hooxit.Data.Repository;

namespace Hooxit.Services.Implementation.ApplicationServices
{
    public class ExperienceService : IExperienceService
    {
        private readonly IUserRepository userRepository;
        private readonly IRepository<Experience> experienceRepository;
        private readonly ICandidateRepository candidateRepository;

        public ExperienceService(IUserRepository userRepository, IUnitOfWork unitOfWork)
        {
            this.userRepository = userRepository;
            this.experienceRepository = unitOfWork.BuildExperienceRepository();
            this.candidateRepository = unitOfWork.BuildCandidateRepository();
        }

        public async Task<ExperienceReadModel> AddExperience(ExperienceWriteModel command)
        {
            var user = await userRepository.GetByName(UserInfo.UserName);
            var userInfo = candidateRepository.GetBydId(user.Id);
            var experience = BuildExperienceModel(command);

            //TODO: Rename
            var currentPositionCandidates = experienceRepository
                .All()
                .Where(x => !x.ToDate.HasValue && x.CandidateID == userInfo.Id);

            if (!currentPositionCandidates.Any())
            {
                userInfo.CurrentPosition = command.Position;
                command.ShouldUpdateUserInfoPosition = true;
            }

            userInfo.Experience.Add(experience);

            await userRepository.Update(user);
            candidateRepository.Save();

            return new ExperienceReadModel(command);
        }

        private Experience BuildExperienceModel(ExperienceWriteModel source)
        {
            return new Experience(source.CompanyName, 
                                  source.Position, 
                                  source.FromDate, 
                                  source.ToDate, 
                                  DateTime.UtcNow);
        }
    }
}

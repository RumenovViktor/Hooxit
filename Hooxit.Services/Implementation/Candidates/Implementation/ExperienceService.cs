using System;
using System.Linq;
using System.Threading.Tasks;

using Hooxit.Data.Contracts;
using Hooxit.Models;
using Hooxit.Data.Repository;
using Hooxit.Services.Candidates.Interfaces;
using Hooxit.Presentation.Implemenation.Candidate.Write;
using Hooxit.Presentation.Implemenation.Candidate.Read;

namespace Hooxit.Services.Implementation.ApplicationServices
{
    public class ExperienceService : IExperienceService
    {
        private readonly IUserRepository userRepository;
        private readonly IReadRepository<Experience> experienceReadRepository;
        private readonly IUpdateRepository<Experience> experienceUpdateRepository;
        private readonly IRepository<Experience> experienceRepository;
        private readonly ICandidateRepository candidateRepository;

        public ExperienceService(IUserRepository userRepository, IUnitOfWork unitOfWork)
        {
            this.userRepository = userRepository;
            this.experienceReadRepository = unitOfWork.BuildExperienceReadRepository();
            this.experienceUpdateRepository = unitOfWork.BuildExperienceUpdateRepository();
            this.candidateRepository = unitOfWork.BuildCandidateRepository();
            this.experienceRepository = unitOfWork.BuildExperienceRepository();
        }

        public async Task<ExperienceReadModel> AddExperience(ExperienceWriteModel command)
        {
            var user = await userRepository.GetByName(UserInfo.UserName);
            var userInfo = candidateRepository.GetById(user.Id);
            var experience = BuildExperienceModel(command);

            //TODO: Rename
            var currentPositionCandidates = experienceReadRepository
                .GetAll()
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

        public async Task<ExperienceReadModel> UpdateExperience(ExperienceWriteModel command)
        {
            var user = await userRepository.GetByName(UserInfo.UserName);
            var userInfo = candidateRepository.GetById(user.Id);
            var experienceEntity = BuildExperienceModel(command);

            var experience = experienceReadRepository.GetById(command.Id);

            experience.CompanyName = command.CompanyName;
            experience.FromDate = command.FromDate.Value;
            experience.ToDate = command.ToDate;
            experience.Position = command.Position;

            experienceUpdateRepository.Update(experience);
            experienceRepository.Save();

            return new ExperienceReadModel(experienceEntity);
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

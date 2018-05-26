using Hooxit.Presentation;
using System.Linq;
using System.Threading.Tasks;
using Hooxit.Data.Contracts;
using System.Collections.Generic;
using Hooxit.Models;
using Hooxit.Data.Repository;
using Hooxit.Models.Users;
using Hooxit.Services.Candidates.Interfaces;

namespace Hooxit.Services.Implementation.Managers
{
    public class ProfileManager : IProfileManager
    {
        private readonly IRepository<Experience> experienceRepository;
        private readonly IReadRepository<Country> countryRepository;
        private readonly IUserRepository userRepository;
        private readonly ICandidateRepository candidateRepository;
        private readonly IRepository<CandidateSkill> candidateSkillRepository;
        private readonly IReadRepository<Skill> skillsReadRepository;

        public ProfileManager(IUserRepository userRepository, IUnitOfWork unitOfWork)
        {
            this.userRepository = userRepository;
            this.experienceRepository = unitOfWork.BuildExperienceRepository();
            this.countryRepository = unitOfWork.BuildCountriesRepository();
            this.candidateRepository = unitOfWork.BuildCandidateRepository();
            this.candidateSkillRepository = unitOfWork.BuildCandidateSkillRepository();
            this.skillsReadRepository = unitOfWork.BuildSkillsReadRepository();
        }

        public async Task<ProfileReadModel> GetProfile(string username)
        {
            var user = await userRepository.GetByName(username);
            var userInfo = candidateRepository.GetBydId(user.Id);
            var candidateSkillsIds = candidateSkillRepository.GetMany(new int[] { userInfo.Id }).Select(x => x.SkillId);
            var candidateSkills = this.skillsReadRepository.GetManyById(candidateSkillsIds.ToArray());

            if (userInfo == null)
            {
                return new ProfileReadModel();
            }

            var country = userInfo.CountryId.HasValue ? countryRepository.GetById(userInfo.CountryId.Value) : null;
            var countryName = country != null ? country.Name : string.Empty;

            var experience = experienceRepository
                .All()
                .Where(x => x.CandidateID == userInfo.Id)
                .OrderByDescending(x => x.CreatedDate)
                .Select(x => new ExperienceReadModel(x))
                .ToList();


            return BuildProfileReadModel(userInfo, experience, countryName, user.Email, candidateSkills);
        }

        private ProfileReadModel BuildProfileReadModel(Candidate user, IList<ExperienceReadModel> experience, string countryName, string email, IList<Skill> skills)
        {
            return new ProfileReadModel(user, experience, countryName, email, skills);
        }
    }
}

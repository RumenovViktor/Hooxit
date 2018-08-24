using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

using Hooxit.Data.Contracts;
using Hooxit.Models;
using Hooxit.Data.Repository;
using Hooxit.Models.Users;
using Hooxit.Services.Candidates.Interfaces;
using Hooxit.Presentation.Implemenation.Candidate.Read;

namespace Hooxit.Services.Implementation.Managers
{
    public class ProfileManager : IProfileManager
    {
        private readonly IReadRepository<Experience> experienceReadRepository;
        private readonly IReadRepository<Country> countryRepository;
        private readonly IUserRepository userRepository;
        private readonly ICandidateRepository candidateRepository;
        private readonly IReadRepository<CandidateSkill> candidateSkillRepository;
        private readonly IReadRepository<Skill> skillsReadRepository;
        private readonly IRepository<PositionCandidate> positionsCandidatesRepository;

        public ProfileManager(IUserRepository userRepository, IUnitOfWork unitOfWork)
        {
            this.userRepository = userRepository;

            experienceReadRepository = unitOfWork.BuildExperienceReadRepository();
            countryRepository = unitOfWork.BuildCountriesRepository();
            candidateRepository = unitOfWork.BuildCandidateRepository();
            candidateSkillRepository = unitOfWork.BuildCandidateSkillReadRepository();
            skillsReadRepository = unitOfWork.BuildSkillsReadRepository();
            positionsCandidatesRepository = unitOfWork.BuildPositionCandidateRepository();
        }

        public async Task<ProfileReadModel> GetProfile(string username)
        {
            var user = await userRepository.GetByName(username);
            var userInfo = candidateRepository.GetById(user.Id);
            var candidateSkillsIds = candidateSkillRepository.GetManyByIds(new int[] { userInfo.Id }).Select(x => x.SkillId);
            var candidateSkills = this.skillsReadRepository.GetManyByIds(candidateSkillsIds.ToArray());

            if (userInfo == null)
            {
                return new ProfileReadModel();
            }

            var country = userInfo.CountryId.HasValue ? countryRepository.GetById(userInfo.CountryId.Value) : null;
            var countryName = country != null ? country.Name : string.Empty;

            var experience = experienceReadRepository
                .GetAll()
                .Where(x => x.CandidateID == userInfo.Id)
                .OrderByDescending(x => x.CreatedDate)
                .Select(x => new ExperienceReadModel(x))
                .ToList();


            return BuildProfileReadModel(userInfo, experience, countryName, user.Email, candidateSkills, user.UserName);
        }

        public async Task<bool> Apply(int positionId)
        {
            var user = await userRepository.GetByName(UserInfo.UserName);
            var userInfo = candidateRepository.GetById(user.Id);

            positionsCandidatesRepository.Add(new PositionCandidate
            {
                CandidateId = userInfo.Id,
                PositionId = positionId
            });

            return true;
        }

        private ProfileReadModel BuildProfileReadModel(Candidate user, IList<ExperienceReadModel> experience, string countryName, string email, IList<Skill> skills, string userName)
        {
            return new ProfileReadModel(user, experience, countryName, email, skills, userName);
        }
    }
}

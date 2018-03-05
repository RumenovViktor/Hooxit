using Hooxit.Presentation;
using Hooxit.Services.Contracts;
using System.Linq;
using Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Hooxit.Data.Contracts;
using System.Collections.Generic;
using Hooxit.Models;
using Hooxit.Data.Repository;
using Hooxit.Models.Users;

namespace Hooxit.Services.Implementation.Managers
{
    public class ProfileManager : IProfileManager
    {
        private readonly IRepository<Experience> experienceRepository;
        private readonly IReadRepository<Country> countryRepository;
        private readonly IUserRepository userRepository;
        private readonly ICandidateRepository candidateRepository;

        public ProfileManager(IUserRepository userRepository, IUnitOfWork unitOfWork)
        {
            this.userRepository = userRepository;
            this.experienceRepository = unitOfWork.BuildExperienceRepository();
            this.countryRepository = unitOfWork.BuildCountriesRepository();
            this.candidateRepository = unitOfWork.BuildCandidateRepository();
        }

        public async Task<ProfileReadModel> GetProfile(string username)
        {
            var user = await userRepository.GetByName(username);
            var userInfo = candidateRepository.GetBydId(user.Id);

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


            return BuildProfileReadModel(userInfo, experience, countryName, user.Email);
        }

        private ProfileReadModel BuildProfileReadModel(Candidate user, IList<ExperienceReadModel> experience, string countryName, string email)
        {
            return new ProfileReadModel(user, experience, countryName, email);
        }
    }
}

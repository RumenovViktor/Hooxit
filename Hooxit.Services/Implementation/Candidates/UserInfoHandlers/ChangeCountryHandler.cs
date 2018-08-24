using Hooxit.Data.Contracts;
using Hooxit.Data.Repository;
using Hooxit.Models;
using Hooxit.Presentation.Implemenation.Candidate.Write;
using Hooxit.Services.Candidates.Interfaces;
using System;
using System.Threading.Tasks;

namespace Hooxit.Services.Candidates.UserInfoHandlers
{
    public class ChangeCountryHandler : IUserPersonalInfoHandler<ChangeCountry>
    {
        private readonly IUserRepository userRepository;
        private readonly ICandidateRepository candidateRepository;
        private readonly IReadRepository<Country> countriesRepository;

        public ChangeCountryHandler(IUserRepository userRepository, IUnitOfWork unitOfWork)
        {
            this.userRepository = userRepository;
            this.countriesRepository = unitOfWork.BuildCountriesRepository();
            this.candidateRepository = unitOfWork.BuildCandidateRepository();
        }

        public async Task<ChangeCountry> Handle(ChangeCountry command)
        {
            var user = await userRepository.GetByName(UserInfo.UserName);
            var country = countriesRepository.GetById(command.CountryId);
            var userInfo = candidateRepository.GetById(user.Id);

            userInfo.CountryId = command.CountryId;

            var updatedUser = await userRepository.Update(user);
            candidateRepository.Save();

            if (updatedUser.Succeeded)
            {
                return command;
            }
            else
            {
                throw new ArgumentException("Something went wrong.", updatedUser.Errors.ToString());
            }
        }
    }
}

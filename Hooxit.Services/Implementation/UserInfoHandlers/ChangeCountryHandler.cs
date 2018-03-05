using Hooxit.Data.Contracts;
using Hooxit.Data.Repository;
using Hooxit.Models;
using Hooxit.Presentation.Write;
using Hooxit.Services.Contracts;
using Microsoft.AspNetCore.Identity;
using Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hooxit.Services.Implementation.Profile
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
            var userInfo = candidateRepository.GetBydId(user.Id);

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

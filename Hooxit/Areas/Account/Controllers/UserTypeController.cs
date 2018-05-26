using Hooxit.Data.Contracts;
using Hooxit.Data.Repository;
using Hooxit.Models.Users;
using Hooxit.Presentation.Write;
using Hooxit.Services.Authentication.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Hooxit.Account.Controllers
{
    [Area("Account")]
    public class UserTypeController : BaseController
    {
        private readonly IUserRepository userRepository;
        private readonly ICandidateRepository candidateRepository;
        private readonly ICompaniesRepository companiesRepository;
        private readonly IAuthenticationProvider authenticationProvider;

        public UserTypeController(IUserRepository userRepository, IAuthenticationProvider authenticationProvider, IUnitOfWork unitOfWork)
        {
            this.userRepository = userRepository;
            this.authenticationProvider = authenticationProvider;
            this.candidateRepository = unitOfWork.BuildCandidateRepository();
            this.companiesRepository = unitOfWork.BuildCompaniesRepository();
        }

        [HttpGet]
        [Route("UserType")]
        public IActionResult ChooseUserType()
        {
            return View(new UserType());
        }

        [HttpPost]
        [Route("UserType")]
        public async Task<IActionResult> SetUserType(UserType userType)
        {
            IActionResult result = null;
            var user = await userRepository.GetByName(User.Identity.Name);

            switch (userType.ChosenUserType)
            {
                case UserTypes.Company:
                    await authenticationProvider.AddClaim(user, "CompanyRole", "Company");
                    companiesRepository.Create(new Company { UserId = user.Id });
                    result = RedirectToAction("Index", "Dashboard", new { area = "Company" }); // TODO: Redirect to company Dashboard
                    break;
                case UserTypes.Candidate:
                    await authenticationProvider.AddClaim(user, "CandidateRole", "Candidate");
                    candidateRepository.Create(new Candidate { UserId = user.Id });
                    result = RedirectToAction("Index", "Dashboard", new { area = "Candidate" });
                    break;
                default:
                    throw new ArgumentException("No user type with this name!");
            }

            await authenticationProvider.SignIn(user);

            return result;
        }
    }
}

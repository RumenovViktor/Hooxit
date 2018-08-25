using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Hooxit.Data.Repository;
using Hooxit.Services.Candidates.Interfaces;
using Hooxit.Services;
using Hooxit.Data.Contracts;
using System;

namespace Hooxit.Controllers
{
    [Area("Candidate")]
    [Authorize(Policy = "CandidatePolicy")]
    public class DashboardController : BaseController
    {
        private readonly IUserRepository userRepository;
        private readonly ICandidateRepository candidateRepository;
        private readonly IDashboardManager dashboardManager;


        public DashboardController(IUserRepository userRepository, IDashboardManager dashboardManager, IUnitOfWork unitOfWork)
        {
            this.userRepository = userRepository;
            this.dashboardManager = dashboardManager;

            candidateRepository = unitOfWork.BuildCandidateRepository();
        }

        [HttpGet]
        [Route("Candidate/Dashboard")]
        public IActionResult Dashboard()
        {
            var user = userRepository.GetByName(UserInfo.UserName);
            var company = candidateRepository.GetById(user.Result.Id);
            
            string companyPicture = null;

            if (company.Picture != null)
            {
                var imageBase64 = Convert.ToBase64String(company.Picture);
                companyPicture = string.Format("data:image/gif;base64,{0}", imageBase64);
            }

            return View("Dashboard", companyPicture);
        }

        [HttpGet]
        [Route("Candidate/SuggestedPositions")]
        public IActionResult SuggestedPositions(int? companyId)
        {
            if (companyId.HasValue)
            {
                var suggestionsForCompany = dashboardManager.GetSuggestions(companyId.Value);
                return View(suggestionsForCompany);
            }

            var suggestions = dashboardManager.GetSuggestions();

            return PartialView(suggestions);
        }
    }
}

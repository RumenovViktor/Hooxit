using System.Collections.Generic;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Hooxit.Data.Contracts;
using Hooxit.Data.Repository;
using Hooxit.Presentation.Implemenation;
using Hooxit.Services.Candidates.Interfaces;

namespace Hooxit.Controllers
{
    [Area("Candidate")]
    [Authorize(Policy = "CandidatePolicy")]
    public class DashboardController : BaseController
    {
        private readonly IUserRepository userRepository;
        private readonly IDashboardManager dashboardManager;

        public DashboardController(IUserRepository userRepository, IDashboardManager dashboardManager)
        {
            this.userRepository = userRepository;
            this.dashboardManager = dashboardManager;
        }

        [HttpGet]
        [Route("Candidate/Dashboard")]
        public IActionResult Dashboard()
        {
            return View(new List<IdNameReadModel>());
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

            return View(suggestions);
        }
    }
}

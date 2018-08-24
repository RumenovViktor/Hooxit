using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Hooxit.Data.Repository;
using Hooxit.Services.Candidates.Interfaces;
using Hooxit.Services;

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
            return View();
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

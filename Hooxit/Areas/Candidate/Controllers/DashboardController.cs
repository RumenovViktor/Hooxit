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
        private readonly ICandidateRepository candidateRepository;
        private readonly IDashboardManager dashboardManagerRepository;

        public DashboardController(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        [HttpGet]
        [Route("Dashboard")]
        public IActionResult Dashboard()
        {
            return View(new List<IdNameReadModel>());
        }

        [HttpGet]
        [Route("Dashboard")]
        public IActionResult SuggestedPositions(int? companyId)
        {
            if (companyId.HasValue)
            {
                // Filter by selected company
            }

            var suggestions = dashboardManagerRepository.GetSuggestions();

            return View(new List<IdNameReadModel>());
        }
    }
}

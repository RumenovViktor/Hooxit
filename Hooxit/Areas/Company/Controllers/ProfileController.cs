using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

using Hooxit.Presentation.Implemenation.Company.Write;
using Hooxit.Services.Candidates.Interfaces;
using Hooxit.Services.Company.Interfaces;

namespace Hooxit.Areas.Company.Controllers
{
    [Area("Company")]
    public class ProfileController : Controller
    {
        private readonly IProfileManager candidateProfileManager;
        private readonly ICompanyProfileManager companyProfileManager;
        private readonly ICompanyProfileService companyProfileApplicationService;

        public ProfileController(IProfileManager candidateProfileManager, ICompanyProfileManager companyProfileManager, ICompanyProfileService companyProfileApplicationService)
        {
            this.candidateProfileManager = candidateProfileManager;
            this.companyProfileManager = companyProfileManager;
            this.companyProfileApplicationService = companyProfileApplicationService;
        }

        [HttpGet]
        [Route("Company/Profile/{username}")]
        public async Task<IActionResult> Profile(string username)
        {
            var profile = await this.companyProfileManager.GetProfile(username);
            return View(profile);
        }

        [HttpGet]
        [Route("ViewCandidate/{*username}")]
        public async Task<IActionResult> ViewCandidate(string username)
        {
            var userProfile = await candidateProfileManager.GetProfile(username);

            return View("ViewCandidateProfile", userProfile);
        }

        [HttpPost]
        [Route("Company/Profile/ChangeDescription")]
        public async Task<IActionResult> ChangeDescription([FromBody] ChangeCompanyDescriptionWrite description)
        {
            if (ModelState.IsValid)
            {
                var response = await companyProfileApplicationService.ChangeDescription(description);
                return Json(true);
            }

            return Json(false);
        }
    }
}
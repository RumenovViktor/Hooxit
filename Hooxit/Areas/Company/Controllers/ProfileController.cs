using Hooxit.Presentation.Company.Write;
using Hooxit.Services.Implementation.Company.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Hooxit.Areas.Company.Controllers
{
    [Area("Company")]
    public class ProfileController : Controller
    {
        private readonly ICompanyProfileManager companyProfileManager;
        private readonly ICompanyProfileApplicationService companyProfileApplicationService;

        public ProfileController(ICompanyProfileManager companyProfileManager, ICompanyProfileApplicationService companyProfileApplicationService)
        {
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
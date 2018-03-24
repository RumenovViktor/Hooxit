using Hooxit.Services.Implementation.Company.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Hooxit.Areas.Company.Controllers
{
    [Area("Company")]
    public class ProfileController : Controller
    {
        private readonly ICompanyProfileManager companyProfileManager;

        public ProfileController(ICompanyProfileManager companyProfileManager)
        {
            this.companyProfileManager = companyProfileManager;
        }

        [HttpGet]
        [Route("Company/Profile/{username}")]
        public async Task<IActionResult> Profile(string username)
        {
            var profile = await this.companyProfileManager.GetProfile(username);
            return View(profile);
        }
    }
}
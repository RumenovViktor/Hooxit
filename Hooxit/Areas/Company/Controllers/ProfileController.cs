using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

using Hooxit.Presentation.Implemenation.Company.Write;
using Hooxit.Services.Candidates.Interfaces;
using Hooxit.Services.Company.Interfaces;
using Hooxit.Presentation.Implementation.Company.Write;
using Hooxit.Services.Implementation.Company.Interfaces;
using Hooxit.Presentation.Implementation.Company.Read;
using Microsoft.AspNetCore.Http;

namespace Hooxit.Areas.Company.Controllers
{
    [Area("Company")]
    public class ProfileController : Controller
    {
        private readonly IProfileManager candidateProfileManager;
        private readonly ICompanyProfileManager companyProfileManager;
        private readonly ICompanyProfileService companyProfileApplicationService;
        private readonly IProductsService productsService;

        public ProfileController(IProfileManager candidateProfileManager, ICompanyProfileManager companyProfileManager, ICompanyProfileService companyProfileApplicationService,
            IProductsService productsService)
        {
            this.candidateProfileManager = candidateProfileManager;
            this.companyProfileManager = companyProfileManager;
            this.companyProfileApplicationService = companyProfileApplicationService;
            this.productsService = productsService;
        }

        [HttpGet]
        [Route("Company/Profile/{username}")]
        public async Task<IActionResult> Profile(string username)
        {
            var profile = await companyProfileManager.GetProfile(username);
            return View(profile);
        }

        [HttpGet]
        [Route("Company/Profile/UploadPicture")]
        public async Task<IActionResult> UploadPicture(IFormFile picture)
        {
            return View();
        }

        [HttpGet]
        [Route("ViewCandidate/{*username}")]
        public async Task<IActionResult> ViewCandidate(string username)
        {
            var candidateProfile = await candidateProfileManager.GetProfile(username);
            var interested = await companyProfileApplicationService.GetInterest(username);

            return View("ViewCandidateProfile", new ViewCandidateProfileReadModel { CandidateProfile = candidateProfile, Interested = interested });
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

        [HttpPost]
        [Route("Company/Profile/CreateProduct")]
        public async Task<IActionResult> CreateProduct([FromBody] ProductWriteModel product)
        {
            if (ModelState.IsValid)
            {
                var createdProduct = await this.productsService.Create(product);

                return Json(createdProduct);
            }

            return Json(false);
        }

        [HttpPost]
        [Route("Company/Profile/ShowInterest")]
        public async Task<IActionResult> ShowInterest(string userName)
        {
            if (ModelState.IsValid)
            {
                var showInteres = await companyProfileApplicationService.ShowInterest(userName);

                return Json(showInteres);
            }

            return Json(false);
        }

        [HttpPost]
        [Route("Company/Profile/NotInterested")]
        public async Task<IActionResult> NotInterested(string userName)
        {
            if (ModelState.IsValid)
            {
                await companyProfileApplicationService.RemoveInterest(userName);
                return Json(true);
            }

            return Json(false);
        }

        [HttpGet]
        [Route("Company/Profile/AllInterested")]
        public async Task<IActionResult> AllInterested()
        {
            var allInterested = await companyProfileApplicationService.AllInterested();
            return View(allInterested);
        }
    }
}
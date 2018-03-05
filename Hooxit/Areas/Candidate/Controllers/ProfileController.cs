using Hooxit.Presentation.Write;
using Hooxit.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Hooxit.Controllers
{
    [Area("Candidate")]
    [Authorize(Policy = "CandidatePolicy")]
    public class ProfileController : BaseController
    {
        private readonly IProfileManager profileManager;
        private readonly IUserPersonalInfoHandler<ChangeEmail> changeEmailHandler;
        private readonly IUserPersonalInfoHandler<ChangeCountry> changeCountryHandler;
        private readonly IUserPersonalInfoHandler<ChangeCurrentPosition> changeCurrentPositionHandler;

        public ProfileController(
            IProfileManager profileManager,
            IUserPersonalInfoHandler<ChangeEmail> changeEmailHandler,
            IUserPersonalInfoHandler<ChangeCountry> changeCountryHandler,
            IUserPersonalInfoHandler<ChangeCurrentPosition> changeCurrentPositionHandler)
        {
            this.profileManager = profileManager;
            this.changeEmailHandler = changeEmailHandler;
            this.changeCountryHandler = changeCountryHandler;
            this.changeCurrentPositionHandler = changeCurrentPositionHandler;
        }

        [HttpGet]
        [Route("Profile/{*username}")]
        public async Task<IActionResult> Profile(string username)
        {
            var userProfile = await profileManager.GetProfile(username);
            return View(userProfile);
        }

        [HttpPost]
        public async Task<IActionResult> ChangePicture()
        {
            return null;
        }

        [HttpPost]
        public async Task<IActionResult> ChangePosition([FromBody]ChangeCurrentPosition position)
        {
            if (ModelState.IsValid)
            {
                var newPosition = await changeCurrentPositionHandler.Handle(position);
                return new JsonResult(new { value = newPosition.Position });
            }

            return Json("Error");
        }

        [HttpPost]
        public async Task<IActionResult> ChangeCountry([FromBody]ChangeCountry country)
        {
            if (ModelState.IsValid)
            {
                var newCountry = await changeCountryHandler.Handle(country);
                return new JsonResult(new { value = new { value = newCountry.CountryId } });
            }

            return Json("Error");
        }

        [HttpPost]
        public async Task<IActionResult> ChangeEmail([FromBody]ChangeEmail email)
        {
            if (ModelState.IsValid)
            {
                var newEmail = await changeEmailHandler.Handle(email);
                return new JsonResult(new { value = newEmail.Email });
            }

            return Json("Error");
        }
    }
}

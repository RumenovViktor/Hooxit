using System.Threading.Tasks;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Hooxit.Presentation.Implemenation.Candidate.Write;
using Hooxit.Services.Candidates.Interfaces;
using Hooxit.Services;
using Hooxit.Data.Repository;
using Hooxit.Services.Company.Interfaces;
using Hooxit.Data.Contracts;
using Hooxit.Models;
using Hooxit.Presentation.Implemenation.Company.Read;
using Hooxit.Presentation.Implementation.Candidate.Write;

namespace Hooxit.Controllers
{
    [Area("Candidate")]
    public class ProfileController : BaseController
    {
        private readonly IProfileManager profileManager;
        private readonly IUserPersonalInfoHandler<ChangeEmail> changeEmailHandler;
        private readonly IUserPersonalInfoHandler<ChangeCountry> changeCountryHandler;
        private readonly IUserPersonalInfoHandler<ChangeCurrentPosition> changeCurrentPositionHandler;
        private readonly ISkillsService skillsService;
        private readonly IPositionsManager positionsManager;

        public ProfileController(
            IProfileManager profileManager,
            IUserPersonalInfoHandler<ChangeEmail> changeEmailHandler,
            IUserPersonalInfoHandler<ChangeCountry> changeCountryHandler,
            IUserPersonalInfoHandler<ChangeCurrentPosition> changeCurrentPositionHandler,
            ISkillsService skillsService,
            IPositionsManager positionsManager)
        {
            this.profileManager = profileManager;
            this.changeEmailHandler = changeEmailHandler;
            this.changeCountryHandler = changeCountryHandler;
            this.changeCurrentPositionHandler = changeCurrentPositionHandler;
            this.skillsService = skillsService;
            this.positionsManager = positionsManager;
        }

        [HttpGet]
        [Route("Profile/{*username}")]
        public async Task<IActionResult> Profile(string username)
        {
            var userProfile = await profileManager.GetProfile(username);
            if (User.Identity.Name.Equals(username))
            {
                return View(userProfile);
            }
            else
            {
                return View("../../Profile/ViewCandidateProfile", userProfile);
            }
        }

        [HttpPost]
        public async Task<IActionResult> ChangePicture()
        {
            return null;
        }

        [HttpPost]
        [Authorize(Policy = "CandidatePolicy")]
        public async Task<IActionResult> ChangePosition([FromBody]ChangeCurrentPosition position)
        {
            if (ModelState.IsValid)
            {
                var newPosition = await changeCurrentPositionHandler.Handle(position);
                return new JsonResult(new { value = newPosition.Position });
            }

            return Json("Error");
        }

        [HttpGet]
        [Route("ViewPosition/{positionId}")]
        [Authorize(Policy = "CandidatePolicy")]
        public IActionResult ViewPosition(int positionId)
        {
            var position = this.positionsManager.GetPositionById(positionId);
            return View(position);
        }

        [HttpPost]
        [Authorize(Policy = "CandidatePolicy")]
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
        [Authorize(Policy = "CandidatePolicy")]
        public async Task<IActionResult> ChangeEmail([FromBody]ChangeEmail email)
        {
            if (ModelState.IsValid)
            {
                var newEmail = await changeEmailHandler.Handle(email);
                return new JsonResult(new { value = newEmail.Email });
            }

            return Json("Error");
        }

        [HttpPost]
        [Authorize(Policy = "CandidatePolicy")]
        public IActionResult ChangeSkills([FromBody] ChangeSkills changeSkills)
        {
            if (ModelState.IsValid)
            {
                skillsService.ChangeSkills(changeSkills);

                return Json(true);
            }

            return Json(false);
        }

        [HttpPost]
        [Authorize(Policy = "CandidatePolicy")]
        public async Task<IActionResult> Apply([FromBody]ApplyForPosition position)
        {
            if (ModelState.IsValid)
            {
                var isSuccessful = await profileManager.Apply(position.PositionId);
                return Json(isSuccessful);
            }

            return Json(false);
        }
    }
}

using System.Threading.Tasks;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Hooxit.Presentation.Implemenation.Candidate.Write;
using Hooxit.Services.Candidates.Interfaces;
using Hooxit.Presentation.Common;
using Hooxit.Presentation.Implemenation.Candidate.Read;

namespace Hooxit.Controllers
{
    [Area("Candidate")]
    [Authorize(Policy = "CandidatePolicy")]
    public class ExperienceController : BaseController
    {
        private readonly IExperienceService experienceApplicationService;

        public ExperienceController(IExperienceService experienceApplicationService)
        {
            this.experienceApplicationService = experienceApplicationService;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Candidate/Experience/AddExperience")]
        public async Task<IActionResult> AddExperience(ExperienceWriteModel experience)
        {
            if (ModelState.IsValid)
            {
                var newExperience = await experienceApplicationService.AddExperience(experience);

                return PartialView("../Experience/_SingleExperiencePartial", newExperience);
            }

            return Json(new CommandExecutionResult<ExperienceWriteModel>(ModelState.IsValid, 
                                                                        "Please fill all required fields."));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Candidate/Experience/UpdateExperience")]
        public async Task<IActionResult> UpdateExperience(ExperienceWriteModel experience)
        {
            if (ModelState.IsValid)
            {
                var newExperience = await experienceApplicationService.UpdateExperience(experience);

                return PartialView("../Experience/_SingleExperiencePartial", newExperience);
            }

            return Json(new CommandExecutionResult<ExperienceReadModel>(ModelState.IsValid,
                                                                        "Please fill all required fields."));
        }
    }
}

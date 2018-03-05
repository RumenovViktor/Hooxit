using Hooxit.Presentation.Read;
using Hooxit.Presentation.Write;
using Hooxit.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

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
    }
}

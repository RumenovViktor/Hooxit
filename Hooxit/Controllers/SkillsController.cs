using Hooxit.Services.Common.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Hooxit.Controllers
{
    public class SkillsController : Controller
    {
        private readonly ICommonDataManager commonDataManager;

        public SkillsController(ICommonDataManager commonDataManager)
        {
            this.commonDataManager = commonDataManager;
        }

        [HttpGet]
        [Route("Skills/GetSkills/{skillName}")]
        public IActionResult GetSkills(string skillName)
        {
            if (ModelState.IsValid)
            {
                var result = this.commonDataManager.GetSkillsByName(skillName);
                return Json(result);
            }

            return View();
        }
    }
}
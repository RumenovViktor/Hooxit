using Hooxit.Services.Contracts;
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
        public IActionResult GetSkills(string skillName)
        {
            if (ModelState.IsValid)
            {
                var result = this.commonDataManager.GetSkillsByName(skillName);
                return PartialView(result);
            }

            return View();
        }
    }
}
using Hooxit.Services;
using Hooxit.Services.Implementation.Company.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Hooxit.Areas.Company.Controllers
{
    [Area("Company")]
    public class PositionSkillRelationController : Controller
    {
        private readonly IPositionSkillRelationManager positionSkillRelationManager;

        public PositionSkillRelationController(IPositionSkillRelationManager positionSkillRelationManager)
        {
            this.positionSkillRelationManager = positionSkillRelationManager;
        }

        [Route("Company/GetRelation")]
        [HttpGet]
        public IActionResult PositionSkillRelation(string id, int matchId)
        {
            var relation = positionSkillRelationManager.GetRelation(id, matchId);

            return View(relation);
        }
    }
}
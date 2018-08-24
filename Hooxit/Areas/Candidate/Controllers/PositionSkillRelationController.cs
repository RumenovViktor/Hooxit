using Hooxit.Services.Company.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Hooxit.Areas.Candidate.Controllers
{
    [Area("Candidate")]
    public class PositionSkillRelationController : Controller
    {
        private readonly IPositionSkillRelationManager positionSkillRelationManager;

        public PositionSkillRelationController(IPositionSkillRelationManager positionSkillRelationManager)
        {
            this.positionSkillRelationManager = positionSkillRelationManager;
        }

        [Route("Candidate/GetCandidateRelation")]
        [HttpGet]
        public IActionResult PositionSkillRelation(int id, string matchId)
        {
            var relation = positionSkillRelationManager.GetRelation(matchId, id);

            return View(relation);
        }
    }
}
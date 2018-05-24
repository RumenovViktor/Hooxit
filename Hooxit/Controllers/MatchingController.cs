using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Hooxit.Controllers
{
    public class MatchingController : Controller
    {
        [HttpGet]
        [Route("Matching/GetCandidateMatches/{positionId}")]
        public async Task<IActionResult> GetCandidateMatches(int positionId)
        {
            return null;
        }
    }
}

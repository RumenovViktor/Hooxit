using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hooxit.Controllers
{
    [Area("Candidate")]
    [Authorize(Policy = "CandidatePolicy")]
    public class DashboardController : BaseController
    {
        [HttpGet]
        [Route("Dashboard")]
        public IActionResult Index()
        {
            return View();
        }
    }
}

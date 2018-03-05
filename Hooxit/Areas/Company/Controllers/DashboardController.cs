using Microsoft.AspNetCore.Mvc;

namespace Hooxit.Areas.Company.Controllers
{
    [Area("Company")]
    public class DashboardController : Controller
    {
        [Route("Company/Dashboard")]
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}
using Microsoft.AspNetCore.Mvc;

namespace Hooxit.Areas.Company.Controllers
{
    [Area("Company")]
    public class ProfileController : Controller
    {
        [HttpGet]
        [Route("Company/Profile/{username}")]
        public IActionResult Profile()
        {
            return View();
        }
    }
}
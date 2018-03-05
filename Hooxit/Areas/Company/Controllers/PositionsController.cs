using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

using Hooxit.Presentation.Write.Company;
using Hooxit.Services.Implementation.Company.Interfaces;

namespace Hooxit.Areas.Company.Controllers
{
    [Area("Company")]
    public class PositionsController : Controller
    {
        private readonly IPositionsManager positionsManager;

        public PositionsController(IPositionsManager positionsManager)
        {
            this.positionsManager = positionsManager;
        }

        [HttpGet]
        [Route("Company/Positions/All/{companyId}")]
        public IActionResult All(string companyId)
        {
            return View();
        }

        [HttpGet]
        [Route("Company/Positions/Create")]
        public IActionResult Create()
        {
            return View("CreatePosition", new CreatePosition());
        }

        [HttpPost]
        [Route("Company/Positions/Create")]
        public async Task<IActionResult> Create(CreatePosition createPosition)
        {
            if (ModelState.IsValid)
            {
                await this.positionsManager.CreatePosition(createPosition);
            }

            return View(createPosition);
        }
    }
}
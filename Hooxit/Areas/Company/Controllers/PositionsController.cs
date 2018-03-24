using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

using Hooxit.Presentation.Write.Company;
using Hooxit.Services.Implementation.Company.Interfaces;
using Hooxit.Data.Repository;
using Hooxit.Services;
using Hooxit.Services.Contracts;
using Hooxit.Data.Contracts;

namespace Hooxit.Areas.Company.Controllers
{
    [Area("Company")]
    public class PositionsController : Controller
    {
        private readonly IUserRepository userRepository;
        private readonly IPositionsApplicationService positionsApplicationService;
        private readonly IPositionsManager positionsManager;
        private readonly IUnitOfWork unitOfWork;
        private readonly ICompaniesRepository companiesRepository;

        public PositionsController(IPositionsApplicationService positionsApplicationService, IPositionsManager positionsManager, IUserRepository userRepository, IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;

            this.positionsApplicationService = positionsApplicationService;
            this.positionsManager = positionsManager;
            this.userRepository = userRepository;
            this.companiesRepository = unitOfWork.BuildCompaniesRepository();
        }

        [HttpGet]
        [Route("Company/Positions/All/{companyId}")]
        public async Task<IActionResult> All(string companyId)
        {
            var user = await this.userRepository.GetByName(UserInfo.UserName);
            var companyInfo = this.companiesRepository.GetBydId(user.Id);

            var allPositions = this.positionsManager.GetAll(companyInfo.Id);

            return View(allPositions);
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
                await this.positionsApplicationService.CreatePosition(createPosition);
            }

            return RedirectToAction("All", "Positions", new { companyId = UserInfo.UserName });
        }
    }
}
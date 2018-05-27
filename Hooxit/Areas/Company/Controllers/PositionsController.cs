using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

using Hooxit.Data.Repository;
using Hooxit.Services;
using Hooxit.Data.Contracts;
using Hooxit.Presentation.Company.Write;
using Hooxit.Services.Company.Interfaces;
using Hooxit.Presentation.Implemenation.Write.Company;
using Hooxit.Presentation.Implemenation.Company.Write;

namespace Hooxit.Areas.Company.Controllers
{
    [Area("Company")]
    public class PositionsController : Controller
    {
        private readonly IUserRepository userRepository;
        private readonly IPositionsService positionsApplicationService;
        private readonly IPositionsManager positionsManager;
        private readonly IUnitOfWork unitOfWork;
        private readonly ICompaniesRepository companiesRepository;

        public PositionsController(IPositionsService positionsApplicationService, IPositionsManager positionsManager, IUserRepository userRepository, IUnitOfWork unitOfWork)
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

        [HttpGet]
        [Route("Company/Positions/View/{userName}/{positionId}")]
        public async Task<IActionResult> View(string userName, int positionId)
        {
            var user = await this.userRepository.GetByName(UserInfo.UserName);
            var companyInfo = this.companiesRepository.GetBydId(user.Id);

            var position = this.positionsManager.GetPosition(companyInfo.Id, positionId);

            return View(position);
        }

        [HttpPost]
        [Route("Company/Positions/ChangeDescription")]
        public IActionResult ChangeDescription([FromBody] ChangeDescription changedPresentationSegment)
        {
            if (ModelState.IsValid)
            {
                var response = this.positionsApplicationService.ChangeDescription(changedPresentationSegment);
                return Json(response);
            }

            return Json(false);
        }

        [HttpPost]
        [Route("Company/Positions/ChangeLookingForDescription")]
        public IActionResult ChangeLookingForDescription([FromBody] ChangeLookingForDescription changedLookingForDescription)
        {
            if (ModelState.IsValid)
            {
                var response = this.positionsApplicationService.ChangeLookingForDescription(changedLookingForDescription);
                return Json(response);
            }

            return Json(false);
        }

        [HttpPost]
        [Route("Company/Positions/ChangeWhatWeOfferDescription")]
        public IActionResult ChangeWhatWeOfferDescription([FromBody] ChangeWhatWeOfferDescription changedWhatWeOfferDescription)
        {
            if (ModelState.IsValid)
            {
                var response = this.positionsApplicationService.ChangeWhatWeOfferDescription(changedWhatWeOfferDescription);
                return Json(response);
            }

            return Json(false);
        }

        [HttpPost]
        [Route("Company/Positions/ChangeResponsibilitiesDescription")]
        public IActionResult ChangeResponsibilitiesDescription([FromBody] ChangeResponsibilitiesDescription changedResponsibilitiesDescription)
        {
            if (ModelState.IsValid)
            {
                var response = this.positionsApplicationService.ChangeResponsibilitiesDescription(changedResponsibilitiesDescription);
                return Json(response);
            }

            return Json(false);
        }

        [HttpPost]
        [Route("Company/Positions/ChangeSkills")]
        public IActionResult ChangeSkills([FromBody] ChangeSkills changeSkills)
        {
            if (ModelState.IsValid)
            {
                var response = this.positionsApplicationService.ChangeSkills(changeSkills);
                return Json(response);
            }

            return Json(false);
        }

        [HttpPost]
        [Route("Company/Positions/ChangePositionName")]
        public IActionResult ChangePositionName([FromBody] ChangePositionName positionName)
        {
            if (ModelState.IsValid)
            {
                var response = this.positionsApplicationService.ChangePositionName(positionName);
                return Json(response);
            }

            return Json(false);
        }
    }
}
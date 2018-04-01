﻿using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

using Hooxit.Presentation.Write.Company;
using Hooxit.Services.Implementation.Company.Interfaces;
using Hooxit.Data.Repository;
using Hooxit.Services;
using Hooxit.Services.Contracts;
using Hooxit.Data.Contracts;
using Hooxit.Presentation.Company.Write;
using Hooxit.Presentation.Company.Contracts;
using Newtonsoft.Json;

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
        public IActionResult ChangeSkills([FromBody] object changeSkills)
        {
            if (ModelState.IsValid)
            {
                var asd = JsonConvert.DeserializeObject<ChangeSkills>(changeSkills.ToString());
                var response = this.positionsApplicationService.ChangeSkills(asd);
                return Json(response);
            }

            return Json(false);
        }
    }
}
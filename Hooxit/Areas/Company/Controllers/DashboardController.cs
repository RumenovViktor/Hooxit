﻿using System.Collections.Generic;

using Microsoft.AspNetCore.Mvc;

using Hooxit.Data.Contracts;
using Hooxit.Data.Repository;
using Hooxit.Services;
using Hooxit.Services.Company.Interfaces;
using Hooxit.Presentation.Implemenation.Company.Read.Matching;

namespace Hooxit.Areas.Company.Controllers
{
    [Area("Company")]
    public class DashboardController : Controller
    {
        private readonly IPositionsManager positionsManager;
        private readonly IUserRepository userRepository;
        private readonly ICompaniesRepository companiesRepository;
        private readonly IDashboardManager dashboardManager;

        public DashboardController(IPositionsManager positionsManager, IUserRepository userRepository, IUnitOfWork unitOfWork, IDashboardManager dashboardManager)
        {
            this.positionsManager = positionsManager;
            this.userRepository = userRepository;
            companiesRepository = unitOfWork.BuildCompaniesRepository();
            this.dashboardManager = dashboardManager;
        }

        [Route("Company/Dashboard")]
        [HttpGet]
        public IActionResult Dashboard()
        {
            var user = userRepository.GetByName(UserInfo.UserName);
            var company = companiesRepository.GetBydId(user.Result.Id);

            var dashboardModel = positionsManager.GetPositionsBasicData(company.Id);

            return View(dashboardModel);
        }

        [Route("Company/SuggestedCandidates/{positionId}")]
        [HttpGet]
        public IActionResult SuggestedCandidates(int? positionId)
        {
            if (positionId.HasValue)
            {
                var suggestedCandidates = dashboardManager.GetSuggestions(positionId.Value);
                return PartialView(suggestedCandidates);
            }

            return PartialView(new List<SuggestedCandidate>());
        }
    }
}
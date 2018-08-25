using System.Collections.Generic;

using Microsoft.AspNetCore.Mvc;

using Hooxit.Data.Contracts;
using Hooxit.Data.Repository;
using Hooxit.Services;
using Hooxit.Services.Company.Interfaces;
using Hooxit.Presentation.Implemenation.Company.Read.Matching;
using System;
using Hooxit.Presentation.Implementation.Company.Read;

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

            var positionsBasicData = positionsManager.GetPositionsBasicData(company.Id);
            string companyPicture = null;

            if (company.Picture != null)
            {
                var imageBase64 = Convert.ToBase64String(company.Picture);
                companyPicture = string.Format("data:image/gif;base64,{0}", imageBase64);
            }

            return View(new CompanyBasicDataReadModel
            {
                IdsNames = positionsBasicData,
                Picture = companyPicture
            });
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
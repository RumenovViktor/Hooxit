using Hooxit.Data.Contracts;
using Hooxit.Data.Repository;
using Hooxit.Presentation.Company.Write;
using Hooxit.Services.Implementation.Company.Interfaces;
using System;
using System.Threading.Tasks;

namespace Hooxit.Services.Implementation.Company.Implementation
{
    public class CompanyProfileApplicationService : ICompanyProfileApplicationService
    {
        private readonly ICompaniesRepository companiesRepository;
        private readonly IUserRepository userRepository;

        public CompanyProfileApplicationService(IUnitOfWork unitOfWork, IUserRepository userRepository)
        {
            this.userRepository = userRepository;
            companiesRepository = unitOfWork.BuildCompaniesRepository();
        }

        public async Task<bool> ChangeDescription(ChangeCompanyDescriptionWrite changeComapnyDescription)
        {
            var user = await userRepository.GetByName(UserInfo.UserName);
            var company = companiesRepository.GetBydId(user.Id);

            try
            {
                company.CompanyDescription = changeComapnyDescription.CompanyDescription;
                companiesRepository.Save();

                return true;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}

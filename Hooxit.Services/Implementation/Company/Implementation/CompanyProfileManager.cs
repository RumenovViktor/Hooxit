using System.Linq;
using System.Threading.Tasks;

using Hooxit.Data.Contracts;
using Hooxit.Data.Repository;
using Hooxit.Presentation.Implemenation.Company.Read;
using Hooxit.Services.Company.Interfaces;

namespace Hooxit.Services.Company.Implemenation
{
    public class CompanyProfileManager : ICompanyProfileManager
    {
        private readonly IUserRepository userRepository;
        private readonly ICompaniesRepository companiesRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IPositionsManager positionsManager;

        public CompanyProfileManager(IUserRepository userRepository, IUnitOfWork unitOfWork, IPositionsManager positionsManager)
        {
            this.unitOfWork = unitOfWork;
            this.userRepository = userRepository;
            this.positionsManager = positionsManager;

            this.companiesRepository = this.unitOfWork.BuildCompaniesRepository();
        }

        public async Task<ProfileInfoRead> GetProfile(string userName)
        {
            var user = await this.userRepository.GetByName(userName);
            var company = this.companiesRepository.GetBydId(user.Id);

            var positionsCount = this.positionsManager.GetAll(company.Id).Count();

            return new ProfileInfoRead
            {
                CreatedPositions = positionsCount,
                CompanyDescription = company.CompanyDescription
            };
        }
    }
}

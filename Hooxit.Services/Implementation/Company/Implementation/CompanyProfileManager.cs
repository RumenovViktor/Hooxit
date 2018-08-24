using System.Linq;
using System.Threading.Tasks;

using Hooxit.Data.Contracts;
using Hooxit.Data.Repository;
using Hooxit.Models;
using Hooxit.Presentation.Implemenation.Company.Read;
using Hooxit.Services.Company.Interfaces;
using Hooxit.Services.Implementation.Company.Interfaces;

namespace Hooxit.Services.Company.Implemenation
{
    public class CompanyProfileManager : ICompanyProfileManager
    {
        private readonly IUserRepository userRepository;
        private readonly ICompaniesRepository companiesRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IPositionsManager positionsManager;
        private readonly IProductsManager productsManager;
        private readonly IReadRepository<CandidateInterest> candidateInterestRepository;

        public CompanyProfileManager(IUserRepository userRepository, IUnitOfWork unitOfWork, IPositionsManager positionsManager, IProductsManager productsManager)
        {
            this.unitOfWork = unitOfWork;
            this.userRepository = userRepository;
            this.positionsManager = positionsManager;
            this.productsManager = productsManager;

            companiesRepository = this.unitOfWork.BuildCompaniesRepository();
            candidateInterestRepository = this.unitOfWork.BuildCandidateInterestReadRepository();
        }

        public async Task<ProfileInfoRead> GetProfile(string userName)
        {
            var user = await this.userRepository.GetByName(userName);
            var company = this.companiesRepository.GetBydId(user.Id);

            var positionsCount = this.positionsManager.GetAll(company.Id).Count();
            var products = this.productsManager.GetAll(company.Id);
            var interestedInCount = candidateInterestRepository.GetManyByIds(new[] { company.Id }).Count;

            return new ProfileInfoRead
            {
                CreatedPositions = positionsCount,
                CompanyDescription = company.CompanyDescription,
                Products = products,
                InterestedInCount = interestedInCount
            };
        }
    }
}

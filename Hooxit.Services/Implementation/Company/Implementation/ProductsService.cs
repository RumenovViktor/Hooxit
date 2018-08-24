using Hooxit.Data.Contracts;
using Hooxit.Data.Repository;
using Hooxit.Models;
using Hooxit.Presentation.Implementation.Company.Write;
using Hooxit.Services.Implementation.Company.Interfaces;
using System.Threading.Tasks;

namespace Hooxit.Services.Implementation.Company.Implementation
{
    public class ProductsService : IProductsService
    {
        private readonly IUserRepository userRepository;
        private readonly ICompaniesRepository companiesRepository;
        private readonly IRepository<Product> productsRepository;


        public ProductsService(IUnitOfWork unitOfWork, IUserRepository userRepository)
        {
            productsRepository = unitOfWork.BuildProductsCreateRepository();
            companiesRepository = unitOfWork.BuildCompaniesRepository();

            this.userRepository = userRepository;
        }

        public async Task<ProductWriteModel> Create(ProductWriteModel product)
        {
            var user = await this.userRepository.GetByName(UserInfo.UserName);
            var company = companiesRepository.GetBydId(user.Id);

            productsRepository.Add(new Product
            {
                CompanyID = company.Id,
                Description = product.Description,
                Name = product.Name,
                Url = product.Url
            });

            //TODO: get product with the ID and return it.
            return product;
        }

        public async Task<ProductWriteModel> Update(ProductWriteModel product)
        {
            throw new System.NotImplementedException();
        }
    }
}

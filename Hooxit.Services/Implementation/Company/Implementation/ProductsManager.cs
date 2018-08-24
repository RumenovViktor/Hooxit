using System.Collections.Generic;
using System.Linq;
using Hooxit.Data.Contracts;
using Hooxit.Models;
using Hooxit.Presentation.Implementation.Company.Read;
using Hooxit.Services.Implementation.Company.Interfaces;

namespace Hooxit.Services.Implementation.Company.Implementation
{
    public class ProductsManager : IProductsManager
    {
        private readonly IReadRepository<Product> productsRepository;

        public ProductsManager(IUnitOfWork unitOfWork)
        {
            productsRepository = unitOfWork.BuildProductsRepository();
        }

        public ProductReadModel Get(int id)
        {
            var product = this.productsRepository.GetById(id);

            return new ProductReadModel(product);
        }

        public IList<ProductReadModel> GetAll(int companyId)
        {
            return this.productsRepository.GetAll().Where(x => x.CompanyID == companyId).Select(x => new ProductReadModel(x)).ToList();
        }
    }
}

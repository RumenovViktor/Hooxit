using Hooxit.Presentation.Implementation.Company.Read;
using System.Collections.Generic;

namespace Hooxit.Services.Implementation.Company.Interfaces
{
    public interface IProductsManager
    {
        IList<ProductReadModel> GetAll(int companyId);
        ProductReadModel Get(int id);
    }
}

using Hooxit.Presentation.Implementation.Company.Write;
using System.Threading.Tasks;

namespace Hooxit.Services.Implementation.Company.Interfaces
{
    public interface IProductsService
    {
        Task<ProductWriteModel> Create(ProductWriteModel product);
        Task<ProductWriteModel> Update(ProductWriteModel product);
    }
}

using Hooxit.Presentation.Company.Write;
using System.Threading.Tasks;

namespace Hooxit.Services.Implementation.Company.Interfaces
{
    public interface ICompanyProfileApplicationService
    {
        Task<bool> ChangeDescription(ChangeCompanyDescriptionWrite changeComapnyDescription);
    }
}

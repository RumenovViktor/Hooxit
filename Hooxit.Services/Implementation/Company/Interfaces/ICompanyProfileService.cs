using Hooxit.Presentation.Company.Write;
using System.Threading.Tasks;

namespace Hooxit.Services.Company.Interfaces
{
    public interface ICompanyProfileService
    {
        Task<bool> ChangeDescription(ChangeCompanyDescriptionWrite changeComapnyDescription);
    }
}

using System.Threading.Tasks;

using Hooxit.Presentation.Implemenation.Company.Write;

namespace Hooxit.Services.Company.Interfaces
{
    public interface ICompanyProfileService
    {
        Task<bool> ChangeDescription(ChangeCompanyDescriptionWrite changeComapnyDescription);
    }
}

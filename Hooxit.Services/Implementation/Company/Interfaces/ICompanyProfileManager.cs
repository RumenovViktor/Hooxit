using System.Threading.Tasks;

using Hooxit.Presentation.Implemenation.Company.Read;

namespace Hooxit.Services.Company.Interfaces
{
    public interface ICompanyProfileManager
    {
        Task<ProfileInfoRead> GetProfile(string userName);
    }
}

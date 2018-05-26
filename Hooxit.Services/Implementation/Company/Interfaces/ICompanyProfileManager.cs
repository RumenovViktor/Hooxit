using Hooxit.Presentation.Company.Read;
using System.Threading.Tasks;

namespace Hooxit.Services.Company.Interfaces
{
    public interface ICompanyProfileManager
    {
        Task<ProfileInfoRead> GetProfile(string userName);
    }
}

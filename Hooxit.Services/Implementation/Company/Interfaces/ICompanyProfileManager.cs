using Hooxit.Presentation.Company.Read;
using System.Threading.Tasks;

namespace Hooxit.Services.Implementation.Company.Interfaces
{
    public interface ICompanyProfileManager
    {
        Task<ProfileInfoRead> GetProfile(string userName);
    }
}

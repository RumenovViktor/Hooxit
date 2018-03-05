using Hooxit.Presentation;
using System.Threading.Tasks;

namespace Hooxit.Services.Contracts
{
    public interface IProfileManager
    {
        Task<ProfileReadModel> GetProfile(string username);
    }
}

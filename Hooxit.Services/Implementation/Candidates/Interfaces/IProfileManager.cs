using Hooxit.Presentation;
using System.Threading.Tasks;

namespace Hooxit.Services.Candidates.Interfaces
{
    public interface IProfileManager
    {
        Task<ProfileReadModel> GetProfile(string username);
    }
}

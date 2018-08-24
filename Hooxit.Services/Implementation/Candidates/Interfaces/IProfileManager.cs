using Hooxit.Presentation.Implemenation.Candidate.Read;
using System.Threading.Tasks;

namespace Hooxit.Services.Candidates.Interfaces
{
    public interface IProfileManager
    {
        Task<ProfileReadModel> GetProfile(string username);
        Task<bool> Apply(int positionId);
    }
}

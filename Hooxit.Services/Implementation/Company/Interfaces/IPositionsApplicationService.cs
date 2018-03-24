using Hooxit.Presentation.Write.Company;
using System.Threading.Tasks;

namespace Hooxit.Services.Implementation.Company.Interfaces
{
    public interface IPositionsApplicationService
    {
        Task<bool> CreatePosition(CreatePosition createPosition);
    }
}

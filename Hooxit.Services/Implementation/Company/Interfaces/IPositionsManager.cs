using Hooxit.Presentation.Write.Company;
using System.Threading.Tasks;

namespace Hooxit.Services.Implementation.Company.Interfaces
{
    public interface IPositionsManager
    {
        Task CreatePosition(CreatePosition createPosition);
    }
}

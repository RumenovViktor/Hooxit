using Hooxit.Presentation.Write;
using Hooxit.Presentation;
using System.Threading.Tasks;

namespace Hooxit.Services.Contracts
{
    public interface IExperienceService
    {
        Task<ExperienceReadModel> AddExperience(ExperienceWriteModel experience);
    }
}

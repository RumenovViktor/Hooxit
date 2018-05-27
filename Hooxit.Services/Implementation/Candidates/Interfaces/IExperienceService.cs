using Hooxit.Presentation;
using System.Threading.Tasks;
using Hooxit.Presentation.Implemenation.Candidate.Write;

namespace Hooxit.Services.Candidates.Interfaces
{
    public interface IExperienceService
    {
        Task<ExperienceReadModel> AddExperience(ExperienceWriteModel experience);
    }
}

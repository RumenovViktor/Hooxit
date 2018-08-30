using System.Threading.Tasks;

using Hooxit.Presentation.Implemenation.Candidate.Write;
using Hooxit.Presentation.Implemenation.Candidate.Read;

namespace Hooxit.Services.Candidates.Interfaces
{
    public interface IExperienceService
    {
        Task<ExperienceReadModel> AddExperience(ExperienceWriteModel experience);
        Task<ExperienceReadModel> UpdateExperience(ExperienceWriteModel command);
    }
}

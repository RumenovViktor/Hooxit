using System.Threading.Tasks;

using Hooxit.Presentation.Implemenation.Company.Write;
using Hooxit.Presentation.Implemenation.Write.Company;
using Hooxit.Presentation.Interfaces.Company;

namespace Hooxit.Services.Company.Interfaces
{
    public interface IPositionsService
    {
        Task<bool> CreatePosition(CreatePosition createPosition);
        bool ChangeDescription(IPresentationSegment presentationSegment);
        bool ChangeLookingForDescription(IPresentationSegment presentationSegment);
        bool ChangeWhatWeOfferDescription(IPresentationSegment presentationSegment);
        bool ChangeResponsibilitiesDescription(IPresentationSegment presentationSegment);
        bool ChangeSkills(ChangeSkills changePositionRequiredSkills);
        bool ChangePositionName(ChangePositionName changePositionName);
    }
}

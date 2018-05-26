using Hooxit.Presentation.Company.Contracts;
using Hooxit.Presentation.Company.Write;
using Hooxit.Presentation.Write.Company;
using System.Threading.Tasks;

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

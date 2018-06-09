using Hooxit.Presentation.Implementation.Candidate.Matching;
using Hooxit.Presentation.Matching;

namespace Hooxit.Services.Candidates.Interfaces
{
    public interface IDashboardManager
    {
        MatchModel<SuggestedPosition, string> GetSuggestions();
        MatchModel<SuggestedPosition, string> GetSuggestions(int companyId);
    }
}

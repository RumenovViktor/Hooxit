using Hooxit.Presentation.Implemenation.Company.Read.Matching;
using Hooxit.Presentation.Matching;

namespace Hooxit.Services.Candidates.Interfaces
{
    public interface IDashboardManager
    {
        MatchModel<SuggestedCandidate, int> GetSuggestions();
    }
}

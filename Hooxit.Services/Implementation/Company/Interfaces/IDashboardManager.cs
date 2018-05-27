using Hooxit.Presentation.Implemenation.Company.Read.Matching;
using Hooxit.Presentation.Matching;

namespace Hooxit.Services.Company.Interfaces
{
    public interface IDashboardManager
    {
        MatchModel<SuggestedCandidate, int> GetSuggestions(int Id);
    }
}

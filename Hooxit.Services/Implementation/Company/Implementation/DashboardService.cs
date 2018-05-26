using Hooxit.Data.Contracts;
using Hooxit.Data.Repository;
using Hooxit.Presentation.Company.Read.Matching;
using Hooxit.Presentation.Read.Matching;
using Hooxit.Services.Company.Interfaces;
using MatchingExecutors;
using MatchingExecutors.Base;

namespace Hooxit.Services.Company.Implemenation
{
    public class DashboardService : IDashboardManager
    {
        private readonly MatchingExecutor<PositionForMatch, SuggestedCandidate> companyMatchingExecutor;

        public DashboardService(IUserRepository userRepository, IUnitOfWork unitOfWork)
        {
            companyMatchingExecutor = new CompanyMatchingExecutor(unitOfWork, userRepository);
        }

        public MatchModel<SuggestedCandidate, int> GetSuggestions(int positionId)
        {
            var suggestedCandidates = companyMatchingExecutor.Match(new PositionForMatch { PositionId = positionId });

            return new MatchModel<SuggestedCandidate, int>
            {
                MatchId = positionId,
                Suggestions = suggestedCandidates
            };
        }
    }
}

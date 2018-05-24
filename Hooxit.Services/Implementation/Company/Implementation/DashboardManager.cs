using Hooxit.Data.Contracts;
using Hooxit.Data.Repository;
using Hooxit.Presentation.Company.Read.Matching;
using Hooxit.Presentation.Read.Matching;
using Hooxit.Services.Implementation.Company.Interfaces;
using MatchingExecutors;
using MatchingExecutors.Base;

namespace Hooxit.Services.Implementation.Company.Implementation
{
    public class DashboardManager : IDashboardManager
    {
        private readonly MatchingExecutor<PositionForMatch, SuggestedCandidate> companyMatchingExecutor;

        public DashboardManager(IUserRepository userRepository, IUnitOfWork unitOfWork)
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

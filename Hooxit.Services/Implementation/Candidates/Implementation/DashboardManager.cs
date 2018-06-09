using Hooxit.Data.Contracts;
using Hooxit.Data.Repository;
using Hooxit.Presentation.Implementation.Candidate.Matching;
using Hooxit.Presentation.Matching;
using Hooxit.Services.Candidates.Interfaces;
using MatchingExecutors;

namespace Hooxit.Services.Implementation.Candidates.Implementation
{
    public class DashboardManager : IDashboardManager
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IUserRepository userRepository;
        private readonly CandidateMatchingExecutor matchingExecutor;

        public DashboardManager(IUserRepository userRepository, IUnitOfWork unitOfWork)
        {
            matchingExecutor = new CandidateMatchingExecutor(userRepository, unitOfWork);
        }

        public MatchModel<SuggestedPosition, string> GetSuggestions()
        {
            var request = new CandidateForMatch() { Username = UserInfo.UserName };
            var suggestions = matchingExecutor.Match(request);

            return new MatchModel<SuggestedPosition, string> { MatchId = UserInfo.UserName, Suggestions = suggestions };
        }

        public MatchModel<SuggestedPosition, string> GetSuggestions(int companyId)
        {
            throw new System.NotImplementedException();
        }
    }
}


using System.Collections.Generic;

namespace MatchingExecutors.Base
{
    public abstract class MatchingExecutor<TMatchRequest, TMatchResult>
    {
        protected const byte Avarage = 2;
        protected const decimal TotalPercentage = 100;

        public abstract IEnumerable<TMatchResult> Match(TMatchRequest matchRequest);
    }
}

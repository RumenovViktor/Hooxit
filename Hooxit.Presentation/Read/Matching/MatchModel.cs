using System.Collections.Generic;

namespace Hooxit.Presentation.Read.Matching
{
    public class MatchModel<T, TId> where T : class, new()
    {
        public TId MatchId { get; set; }

        public IEnumerable<T> Suggestions { get; set; }
    }
}

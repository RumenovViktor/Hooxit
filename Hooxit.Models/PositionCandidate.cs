using Hooxit.Models.Users;

namespace Hooxit.Models
{
    public class PositionCandidate
    {
        public int CandidateId { get; set; }
        public Candidate Candidate { get; set; }

        public int PositionId { get; set; }
        public Position Position { get; set; }
    }
}

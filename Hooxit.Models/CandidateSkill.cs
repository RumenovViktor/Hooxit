using Hooxit.Models.Users;

namespace Hooxit.Models
{
    public class CandidateSkill
    {
        public int CandidateId { get; set; }
        public Candidate Candidate { get; set; }

        public int SkillId { get; set; }
        public Skill Skill { get; set; }
    }
}

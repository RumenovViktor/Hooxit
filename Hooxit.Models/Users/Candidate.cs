using Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Hooxit.Models.Users
{
    public class Candidate
    {
        public Candidate()
        {
            this.Experience = new HashSet<Experience>();
        }

        [Key]
        public virtual int Id { get; set; }

        public virtual string UserId { get; set; }
        public virtual User User { get; set; }

        public virtual string FirstName { get; set; }
        public virtual string CurrentPosition { get; set; }
        public virtual string LastName { get; set; }
        public virtual int? CountryId { get; set; }
        public virtual Country Country { get; set; }

        public ICollection<Experience> Experience { get; set; }
        public virtual ICollection<CandidateSkill> CandidateSkill { get; set; }
        public virtual ICollection<PositionCandidate> PositionCandidate { get; set; }
        public virtual byte[] Picture { get; set; }
    }
}

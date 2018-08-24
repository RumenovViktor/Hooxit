using Hooxit.Models.Users;
using System.ComponentModel.DataAnnotations;

namespace Hooxit.Models
{
    public class CandidateInterest
    {
        [Key]
        public virtual int ID { get; set; }

        public virtual int CompanyId { get; set; }

        public virtual Company Company { get; set; }

        public virtual int CandidateId { get; set; }

        public virtual Candidate Candidate { get; set; }
    }
}

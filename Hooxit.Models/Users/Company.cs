using Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Hooxit.Models.Users
{
    public class Company
    {
        public Company()
        {
            Positions = new HashSet<Position>();
            Products = new HashSet<Product>();
            CandidateInterests = new HashSet<CandidateInterest>();
        }

        [Key]
        public virtual int Id { get; set; }

        public virtual string UserId { get; set; }

        public virtual User User { get; set; }

        public virtual string CompanyDescription { get; set; }

        public virtual ICollection<Position> Positions { get; set; }

        public virtual ICollection<Product> Products { get; set; }

        public virtual ICollection<CandidateInterest> CandidateInterests { get; set; }

        public virtual byte[] Picture { get; set; }
    }
}

using Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Hooxit.Models.Users
{
    public class Company
    {
        public Company()
        {
            this.Positions = new HashSet<Position>();
        }

        [Key]
        public virtual int Id { get; set; }

        public virtual string UserId { get; set; }

        public virtual User User { get; set; }

        public virtual string CompanyDescription { get; set; }

        public virtual ICollection<Position> Positions { get; set; }
    }
}

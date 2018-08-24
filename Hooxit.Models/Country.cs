using Hooxit.Models.Users;
using Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hooxit.Models
{
    public class Country
    {
        public Country()
        {
            this.Candidates = new HashSet<Candidate>();
        }

        [Key]
        public virtual int Id { get; set; }
        public virtual string Iso { get; set; }
        public virtual string Name { get; set; }
        public virtual string Iso3 { get; set; }
        public virtual int? Numcode { get; set; }
        public virtual int? Phonecode { get; set; }

        public virtual ICollection<Candidate> Candidates { get; set; }
    }
}

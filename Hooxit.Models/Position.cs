using Hooxit.Models.Users;
using System.Collections.Generic;

namespace Hooxit.Models
{
    public class Position
    {
        public Position()
        {
            this.PositionSkill = new HashSet<PositionSkill>();
            PositionCandidate = new HashSet<PositionCandidate>();
        }

        public virtual int PositionID { get; set; }

        public virtual string PositionName { get; set; }

        public virtual string Description { get; set; }

        public virtual int? MinimumYearsOfExperience { get; set; }
        
        public virtual string LookingFor { get; set; }
        
        public virtual string Responsibilities { get; set; }
        
        public virtual string WhatWeOffer { get; set; }

        public virtual ICollection<PositionSkill> PositionSkill { get; set; }
        
        public virtual int CompanyID { get; set; }

        public virtual Company Company { get; set; }

        public virtual ICollection<PositionCandidate> PositionCandidate { get; set; }
    }
}

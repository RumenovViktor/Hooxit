using Hooxit.Models.Users;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hooxit.Models
{
    public class Experience
    {
        public Experience() { }

        public Experience(string companyName, string position, DateTime? fromDate, DateTime? toDate, DateTime createdDate)
        {
            this.CompanyName = companyName;
            this.Position = position;
            this.FromDate = fromDate.Value;
            this.ToDate = toDate;
            this.CreatedDate = createdDate;
        }

        public virtual int ExperienceID { get; set; }
        public virtual string CompanyName { get; set; }
        public virtual string Position { get; set; }
        public virtual string Description { get; set; }
        public virtual DateTime FromDate { get; set; }
        public virtual DateTime? ToDate { get; set; }
        public virtual DateTime? CreatedDate { get; set; }
        
        public virtual int CandidateID { get; set; }
        public virtual Candidate Candidate { get; set; }
    }
}

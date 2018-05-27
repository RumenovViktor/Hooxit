using Hooxit.Presentation.Interfaces;
using System;

namespace Hooxit.Presentation.Implemenation.Candidate.Write
{
    public class ChangeCountry : ICommand
    {
        public ChangeCountry() { }

        public ChangeCountry(int countryId, string issuedBy)
        {
            this.CountryId = countryId;
            this.IssuedBy = issuedBy;
            this.IssuedOn = DateTime.UtcNow;
        }
        
        public int CountryId { get; set; }
        public string IssuedBy { get; set; }
        public DateTime IssuedOn { get; set; }
    }
}

using System;
using System.ComponentModel.DataAnnotations;

namespace Hooxit.Presentation.Write
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

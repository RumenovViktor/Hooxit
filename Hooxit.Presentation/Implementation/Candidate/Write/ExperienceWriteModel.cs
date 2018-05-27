using System;
using System.ComponentModel.DataAnnotations;

using Hooxit.Presentation.Interfaces;

namespace Hooxit.Presentation.Implemenation.Candidate.Write
{
    public class ExperienceWriteModel : ICommand
    {
        [Required]
        public DateTime? FromDate { get; set; }

        public DateTime? ToDate { get; set; }

        [Required]
        public string Position { get; set; }

        [Required]
        public string CompanyName { get; set; }

        public bool ShouldUpdateUserInfoPosition { get; set; }

        public DateTime IssuedOn { get; set; }
    }
}

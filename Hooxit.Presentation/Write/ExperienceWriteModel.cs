using System;
using System.ComponentModel.DataAnnotations;

namespace Hooxit.Presentation.Write
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

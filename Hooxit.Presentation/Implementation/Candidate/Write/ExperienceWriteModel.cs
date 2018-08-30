using System;
using System.ComponentModel.DataAnnotations;
using Hooxit.Presentation.Implemenation.Candidate.Read;
using Hooxit.Presentation.Interfaces;

namespace Hooxit.Presentation.Implemenation.Candidate.Write
{
    public class ExperienceWriteModel : ICommand
    {
        public ExperienceWriteModel() { }

        public ExperienceWriteModel(ExperienceReadModel readModel)
        {
            Id = readModel.Id;
            FromDate = readModel.FromDate;
            ToDate = readModel.ToDate;
            Position = readModel.Position;
            CompanyName = readModel.CompanyName;
        }

        public int Id { get; set; }

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

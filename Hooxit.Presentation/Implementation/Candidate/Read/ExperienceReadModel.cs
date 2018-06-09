using System;

using Hooxit.Models;
using Hooxit.Presentation.Implemenation.Candidate.Write;

namespace Hooxit.Presentation.Implemenation.Candidate.Read
{
    public class ExperienceReadModel
    {
        public ExperienceReadModel() { }

        public ExperienceReadModel(ExperienceWriteModel source)
        {
            FromDate = source.FromDate;
            ToDate = source.ToDate;
            Position = source.Position;
            CompanyName = source.CompanyName;
            ShouldUpdateUserInfoPosition = source.ShouldUpdateUserInfoPosition;
        }

        public ExperienceReadModel(Experience source)
        {
            FromDate = source.FromDate;
            ToDate = source.ToDate;
            Position = source.Position;
            CompanyName = source.CompanyName;
            Description = source.Description;
        }

        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public string Position { get; set; }
        public string CompanyName { get; set; }
        public string Description { get; set; }
        public string Username { get; set; }
        public bool ShouldUpdateUserInfoPosition { get; set; }
    }
}

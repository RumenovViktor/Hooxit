using Hooxit.Models.Users;
using Hooxit.Utils.Constants;
using Models;
using System.Collections.Generic;

namespace Hooxit.Presentation
{
    public class ProfileReadModel
    {
        public ProfileReadModel()
        {

        }

        public ProfileReadModel(Candidate source, IList<ExperienceReadModel> experience, string countryName, string email)
        {
            Email = email;
            //FullName = source.FirstName + CommonConstants.Space + source.LastName;
            Position = string.IsNullOrWhiteSpace(source.CurrentPosition) ? ProfileConstants.NoPosition : source.CurrentPosition;
            Experience = experience;
            Country = string.IsNullOrWhiteSpace(countryName) ? ProfileConstants.NoCountry : countryName;
        }

        public string FullName { get; set; }
        public string Position { get; set; }
        public string Country { get; set; }
        public string Email { get; set; }

        public ICollection<ExperienceReadModel> Experience { get; set; }
    }
}

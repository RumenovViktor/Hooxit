using Hooxit.Models;
using Hooxit.Models.Users;
using Hooxit.Presentation.Read;
using Hooxit.Utils.Constants;
using Models;
using System.Collections.Generic;
using System.Linq;

namespace Hooxit.Presentation
{
    public class ProfileReadModel
    {
        public ProfileReadModel()
        {

        }

        public ProfileReadModel(Candidate source, IList<ExperienceReadModel> experience, string countryName, string email, IList<Skill> skills)
        {
            Email = email;
            //FullName = source.FirstName + CommonConstants.Space + source.LastName;
            Position = string.IsNullOrWhiteSpace(source.CurrentPosition) ? ProfileConstants.NoPosition : source.CurrentPosition;
            Experience = experience;
            Country = string.IsNullOrWhiteSpace(countryName) ? ProfileConstants.NoCountry : countryName;
            Skills = skills.Select(x => new SkillReadModel(x)).ToList();
        }

        public string FullName { get; set; }
        public string Position { get; set; }
        public string Country { get; set; }
        public string Email { get; set; }
        public IList<SkillReadModel> Skills { get; set; }

        public ICollection<ExperienceReadModel> Experience { get; set; }
    }
}

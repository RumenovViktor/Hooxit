using System.Linq;
using System.Collections.Generic;

using Hooxit.Models;
using Hooxit.Presentation.Implementation;
using Hooxit.Utils.Constants;

namespace Hooxit.Presentation.Implemenation.Candidate.Read
{
    using Models.Users;
    using System;

    public class ProfileReadModel
    {
        public ProfileReadModel() { }

        public ProfileReadModel(Candidate source, IList<ExperienceReadModel> experience, string countryName, string email, IList<Skill> skills, string userName, byte[] picture)
        {
            Email = email;
            //FullName = source.FirstName + CommonConstants.Space + source.LastName;
            Position = string.IsNullOrWhiteSpace(source.CurrentPosition) ? ProfileConstants.NoPosition : source.CurrentPosition;
            Experience = experience;
            Country = string.IsNullOrWhiteSpace(countryName) ? ProfileConstants.NoCountry : countryName;
            Skills = skills.Select(x => new SkillReadModel(x)).ToList();
            UserName = userName;
            string pic = null;

            if (picture != null)
            {
                var imageBase64 = Convert.ToBase64String(picture);
                pic = string.Format("data:image/gif;base64,{0}", imageBase64);
            }

            Picture = pic;
        }

        public string FullName { get; set; }
        public string Position { get; set; }
        public string Country { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public IList<SkillReadModel> Skills { get; set; }
        public string Picture { get; set; }

        public ICollection<ExperienceReadModel> Experience { get; set; }
    }
}

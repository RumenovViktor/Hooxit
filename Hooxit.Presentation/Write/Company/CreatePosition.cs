using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Hooxit.Presentation.Write.Company
{
    public class CreatePosition
    {
        [Required]
        public string PositionName { get; set; }

        [Required]
        public string Description { get; set; }

        public int? MinimumYearsOfExperience { get; set; }

        [Required]
        public string LookingFor { get; set; }

        [Required]
        public string Responsibilities { get; set; }

        [Required]
        public string WhatWeOffer { get; set; }

        public IList<int> RequiredSkills { get; set; }

        //public IList<int> RecommendedSkills { get; set; }
    }
}

using System.Collections.Generic;

namespace Hooxit.Presentation.Company.Read
{
    public class PositionReadModel
    {
        public string PositionName { get; set; }
        
        public string Description { get; set; }

        public int? MinimumYearsOfExperience { get; set; }
        
        public string LookingFor { get; set; }
        
        public string Responsibilities { get; set; }
        
        public string WhatWeOffer { get; set; }

        public IList<IdNameReadModel> RequiredSkills { get; set; }
    }
}

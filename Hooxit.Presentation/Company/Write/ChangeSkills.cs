using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Hooxit.Presentation.Company.Write
{
    [Serializable]
    public class ChangeSkills
    {
        [Required]
        public int PositionId { get; set; }

        [Required]
        public int[] RequiredSkills { get; set; }
    }
}

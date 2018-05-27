using System;
using System.ComponentModel.DataAnnotations;

namespace Hooxit.Presentation.Implemenation.Company.Write
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

using System.ComponentModel.DataAnnotations;

namespace Hooxit.Presentation.Implemenation.Company.Write
{
    public class ChangePositionName
    {
        [Required]
        public int PositionId { get; set; }

        [Required]
        [MinLength(1)]
        public string PositionName { get; set; }
    }
}

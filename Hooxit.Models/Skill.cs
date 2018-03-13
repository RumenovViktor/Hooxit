using System.ComponentModel.DataAnnotations;

namespace Hooxit.Models
{
    public class Skill
    {
        [Key]
        public int ID { get; set; }

        public string Name { get; set; }
    }
}

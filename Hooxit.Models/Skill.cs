using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Hooxit.Models
{
    public class Skill
    {
        public Skill()
        {
            this.PositionSkill = new HashSet<PositionSkill>();
        }

        [Key]
        public int ID { get; set; }

        public string SkillName { get; set; }

        public ICollection<PositionSkill> PositionSkill { get; set; }
    }
}

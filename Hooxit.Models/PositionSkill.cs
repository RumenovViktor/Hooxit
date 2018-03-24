namespace Hooxit.Models
{
    public class PositionSkill
    {
        public int PositionId { get; set; }
        public Position Position { get; set; }

        public int SkillId { get; set; }
        public Skill Skill { get; set; }
    }
}

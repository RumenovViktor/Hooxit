using Hooxit.Models;

namespace Hooxit.Presentation.Implementation
{
    public class SkillReadModel
    {
        public SkillReadModel(Skill skill)
        {
            this.ID = skill.ID;
            this.Name = skill.SkillName;
        }

        public int ID { get; set; }

        public string Name { get; set; }
    }
}

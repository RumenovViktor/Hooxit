using Hooxit.Models;

namespace Hooxit.Presentation.Read
{
    public class SkillReadModel
    {
        public SkillReadModel(Skill skill)
        {
            this.ID = skill.ID;
            this.Name = skill.Name;
        }

        public int ID { get; set; }

        public string Name { get; set; }
    }
}

using System.Collections.Generic;

namespace Hooxit.Presentation.Company.Read
{
    public class RelationRead<T> where T : class, new()
    {
        public string Name { get; set; }

        public IEnumerable<T> SkillsRelation { get; set; }
    }
}

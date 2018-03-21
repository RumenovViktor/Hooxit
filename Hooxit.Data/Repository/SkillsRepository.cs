using Data;
using Hooxit.Data.Contracts;
using Hooxit.Models;
using System.Collections.Generic;
using System.Linq;

namespace Hooxit.Data.Repository
{
    public class SkillsRepository : IReadByNameRepository<Skill>
    {
        private readonly IHooxitDbContext dbContext;

        public SkillsRepository(IHooxitDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IEnumerable<Skill> GetByName(string name)
        {
            return dbContext.Skills.Where(x => x.SkillName.StartsWith(name)).ToList();
        }
    }
}

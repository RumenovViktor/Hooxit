using Data;
using Hooxit.Data.Contracts;
using Hooxit.Models;
using System.Collections.Generic;
using System.Linq;

namespace Hooxit.Data.Repository
{
    public class SkillsRepository : IReadByNameRepository<Skill>, IReadRepository<Skill>
    {
        private readonly IHooxitDbContext dbContext;

        public SkillsRepository(IHooxitDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IList<Skill> GetAll()
        {
            throw new System.NotImplementedException();
        }

        public Skill GetById(int id)
        {
            return this.dbContext.Skills.FirstOrDefault(x => x.ID == id);
        }

        public IList<Skill> GetManyById(int[] ids)
        {
            return this.dbContext.Skills.Where(x => ids.Contains(x.ID)).ToList();
        }

        public IEnumerable<Skill> GetByName(string name)
        {
            return dbContext.Skills.Where(x => x.SkillName.StartsWith(name)).ToList();
        }
    }
}

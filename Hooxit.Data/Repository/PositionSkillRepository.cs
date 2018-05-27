using System.Collections.Generic;
using System.Linq;
using Data;
using Hooxit.Data.Contracts;
using Hooxit.Models;

namespace Hooxit.Data.Repository
{
    public class PositionSkillRepository : IReadRepository<PositionSkill>
    {
        private readonly IHooxitDbContext dbContext;

        public PositionSkillRepository(IHooxitDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IList<PositionSkill> GetAll()
        {
            return this.dbContext.PositionSkill.ToList();
        }

        public PositionSkill GetById(int id)
        {
            return this.dbContext.PositionSkill.FirstOrDefault(x => x.PositionId == id);
        }

        public IList<PositionSkill> GetManyByIds(int[] ids)
        {
            return this.dbContext.PositionSkill.Where(x => ids.Contains(x.PositionId)).ToList();
        }
    }
}

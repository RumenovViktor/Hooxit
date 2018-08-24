using System.Collections.Generic;
using System.Linq;
using Data;
using Hooxit.Data.Contracts;
using Hooxit.Models;

namespace Hooxit.Data.Repository
{
    public class PositionCandidateRepository : IRepository<PositionCandidate>, IReadRepository<PositionCandidate>
    {
        private readonly IHooxitDbContext dbContext;

        public PositionCandidateRepository(IHooxitDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void Add(PositionCandidate entity)
        {
            this.dbContext.PositionsCandidates.Add(entity);
            Save();
        }

        public IList<PositionCandidate> GetAll()
        {
            return dbContext.PositionsCandidates.ToList();
        }

        public PositionCandidate GetById(int id)
        {
            return dbContext.PositionsCandidates.Where(x => x.PositionId == id).FirstOrDefault();
        }

        public IList<PositionCandidate> GetManyByIds(int[] ids)
        {
            return dbContext.PositionsCandidates.Where(x => ids.Contains(x.PositionId)).ToList();
        }

        public void Save()
        {
            dbContext.SaveChanges();
        }
    }
}

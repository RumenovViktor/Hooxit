using System.Collections.Generic;
using System.Linq;
using Data;
using Hooxit.Data.Contracts;
using Hooxit.Models;

namespace Hooxit.Data.Repository
{
    public class PositionsRepository : IRepository<Position>
    {
        private readonly IHooxitDbContext dbContext;

        public PositionsRepository(IHooxitDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void Add(Position entity)
        {
            this.dbContext.Positions.Add(entity);
        }

        public IList<Position> All()
        {
            return this.dbContext.Positions.ToList();
        }

        public void Delete(Position entity)
        {

        }

        public Position Get(int id)
        {
            return this.dbContext.Positions.Where(x => x.PositionID == id).FirstOrDefault();
        }

        public IQueryable GetMany(int[] id)
        {
            return this.dbContext.Positions.Where(x => id.Contains(x.PositionID));
        }

        public void Update(Position entity)
        {
            dbContext.Positions.Update(entity);
        }
    }
}

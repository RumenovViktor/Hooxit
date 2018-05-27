using System.Collections.Generic;
using System.Linq;

using Data;
using Hooxit.Data.Contracts;
using Hooxit.Models;

namespace Hooxit.Data.Repository
{
    public class PositionsRepository 
        : IRepository<Position>,
          IUpdateRepository<Position>,
          IReadRepository<Position>
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

        public IList<Position> GetAll()
        {
            return this.dbContext.Positions.ToList();
        }

        public Position GetById(int id)
        {
            return this.dbContext.Positions.Where(x => x.PositionID == id).FirstOrDefault();
        }

        public IList<Position> GetManyByIds(int[] ids)
        {
            return this.dbContext.Positions.Where(x => ids.Contains(x.PositionID)).ToList();
        }

        public void Update(Position entity)
        {
            dbContext.Positions.Update(entity);
        }

        public void Save()
        {
            dbContext.SaveChanges();
        }
    }
}

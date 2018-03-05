using System.Linq;
using Hooxit.Data.Contracts;
using Hooxit.Models;
using Data;
using System.Collections.Generic;

namespace Hooxit.Data.Repository
{
    public class ExperienceRepository : IRepository<Experience>
    {
        private readonly IHooxitDbContext dbContext;

        public ExperienceRepository(IHooxitDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IList<Experience> All()
        {
            return dbContext.Experience.ToList();
        }

        public void Add(Experience entity)
        {
            dbContext.Experience.Add(entity);
        }

        public void Delete(Experience entity)
        {
        }

        public Experience Get(int id)
        {
            return dbContext.Experience.Where(x => x.ExperienceID == id).SingleOrDefault();
        }

        public IQueryable GetMany(int[] id)
        {
            return dbContext.Experience.Where(x => id.Contains(x.ExperienceID));
        }

        public void Update(Experience entity)
        {
            dbContext.Experience.Update(entity);
        }
    }
}

using System.Linq;
using Hooxit.Data.Contracts;
using Hooxit.Models;
using Data;
using System.Collections.Generic;

namespace Hooxit.Data.Repository
{
    public class ExperienceRepository 
        : IRepository<Experience>, 
          IUpdateRepository<Experience>,
          IReadRepository<Experience>
    {
        private readonly IHooxitDbContext dbContext;

        public ExperienceRepository(IHooxitDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IList<Experience> GetAll()
        {
            return dbContext.Experience.ToList();
        }

        public void Add(Experience entity)
        {
            dbContext.Experience.Add(entity);
        }

        public Experience GetById(int id)
        {
            return dbContext.Experience.Where(x => x.ExperienceID == id).SingleOrDefault();
        }

        public IList<Experience> GetManyByIds(int[] id)
        {
            return dbContext.Experience.Where(x => id.Contains(x.ExperienceID)).ToList();
        }

        public void Update(Experience entity)
        {
            dbContext.Experience.Update(entity);
        }

        public void Save()
        {
            dbContext.SaveChanges();
        }
    }
}

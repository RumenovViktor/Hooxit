using Data;
using Hooxit.Data.Contracts;
using Hooxit.Models;
using System.Collections.Generic;
using System.Linq;

namespace Hooxit.Data.Repository
{
    public class CandidateInterestsRepository : IRepository<CandidateInterest>, IReadRepository<CandidateInterest>, IDeleteRepository<CandidateInterest>
    {
        private readonly IHooxitDbContext dbContext;

        public CandidateInterestsRepository(IHooxitDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void Add(CandidateInterest entity)
        {
            dbContext.CandidateInterests.Add(entity);

            Save();
        }

        public void Delete(CandidateInterest entity)
        {
            dbContext.CandidateInterests.Remove(entity);
        }

        public IList<CandidateInterest> GetAll()
        {
            return dbContext.CandidateInterests.ToList();
        }

        public CandidateInterest GetById(int companyId)
        {
            return dbContext.CandidateInterests.FirstOrDefault(x => x.CompanyId == companyId);
        }

        public IList<CandidateInterest> GetManyByIds(int[] ids)
        {
            return dbContext.CandidateInterests.Where(x => ids.Contains(x.CompanyId)).ToList();
        }

        public void Save()
        {
            dbContext.SaveChanges();
        }
    }
}

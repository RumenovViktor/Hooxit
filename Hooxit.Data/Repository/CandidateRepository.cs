using System.Collections.Generic;
using System.Linq;

using Data;
using Hooxit.Data.Contracts;
using Hooxit.Models.Users;

namespace Hooxit.Data.Repository
{
    public class CandidateRepository : ICandidateRepository
    {
        private readonly IHooxitDbContext context;

        public CandidateRepository(IHooxitDbContext context)
        {
            this.context = context;
        }

        public void Create(Candidate candidate)
        {
            this.context.Candidates.Add(candidate);

            this.context.SaveChanges();
        }

        public IQueryable<Candidate> GetAll()
        {
            return this.context.Candidates;
        }

        public Candidate GetById(int id)
        {
            return context.Candidates.FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<Candidate> GetManyByIds(int[] id)
        {
            return context.Candidates.Where(x => id.Contains(x.Id)).ToList();
        }

        public Candidate GetById(string id)
        {
            return context.Candidates.FirstOrDefault(x => x.UserId == id);
        }

        public void Update(Candidate candidate)
        {
            context.Candidates.Update(candidate);
        }

        public void Save()
        {
            this.context.SaveChanges();
        }
    }
}

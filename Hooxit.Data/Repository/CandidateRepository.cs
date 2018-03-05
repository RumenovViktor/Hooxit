using Data;
using Hooxit.Data.Contracts;
using Hooxit.Models.Users;
using System.Linq;

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

        public Candidate GetBydId(string id)
        {
            return context.Candidates.FirstOrDefault(x => x.UserId == id);
        }

        public void Save()
        {
            this.context.SaveChanges();
        }
    }
}

using Hooxit.Models.Users;
using System.Collections.Generic;
using System.Linq;

namespace Hooxit.Data.Contracts
{
    public interface ICandidateRepository
    {
        IQueryable<Candidate> GetAll();
        void Create(Candidate candidate);
        Candidate GetById(int id);
        Candidate GetById(string id);
        IEnumerable<Candidate> GetManyByIds(int[] id);
        void Update(Candidate candidate);
        void Save();
    }
}

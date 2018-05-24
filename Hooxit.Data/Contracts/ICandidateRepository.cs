using Hooxit.Models.Users;
using System.Linq;

namespace Hooxit.Data.Contracts
{
    public interface ICandidateRepository
    {
        IQueryable<Candidate> GetAll();
        void Create(Candidate candidate);
        Candidate GetBydId(string id);
        void Save();
    }
}

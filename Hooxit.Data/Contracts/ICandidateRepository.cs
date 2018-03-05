using Hooxit.Models.Users;

namespace Hooxit.Data.Contracts
{
    public interface ICandidateRepository
    {
        void Create(Candidate candidate);
        Candidate GetBydId(string id);
        void Save();
    }
}

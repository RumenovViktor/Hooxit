using Hooxit.Models.Users;

namespace Hooxit.Data.Contracts
{
    public interface ICompaniesRepository
    {
        void Create(Company company);
        Company GetBydId(string id);
        void Update(Company company);
        void Save();
    }
}

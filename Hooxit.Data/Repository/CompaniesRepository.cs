using System.Linq;

using Data;
using Hooxit.Data.Contracts;
using Hooxit.Models.Users;

namespace Hooxit.Data.Repository
{
    public class CompaniesRepository : ICompaniesRepository
    {
        private readonly IHooxitDbContext context;

        public CompaniesRepository(IHooxitDbContext context)
        {
            this.context = context;
        }

        public void Create(Company company)
        {
            this.context.Companies.Add(company);
            Save();
        }

        public Company GetBydId(string id)
        {
            return this.context.Companies.Where(x => x.UserId == id).FirstOrDefault();
        }

        public void Update(Company company)
        {
            context.Companies.Update(company);
        }

        public void Save()
        {
            this.context.SaveChanges();
        }
    }
}

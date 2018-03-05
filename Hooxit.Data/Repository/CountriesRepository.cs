using Data;
using Hooxit.Data.Contracts;
using Hooxit.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hooxit.Data.Repository
{
    public class CountriesRepository : IReadRepository<Country>
    {
        private readonly IHooxitDbContext dbContext;

        public CountriesRepository(IHooxitDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IList<Country> GetAll()
        {
            return dbContext.Countries.ToList();
        }

        public Country GetById(int id)
        {
            return dbContext.Countries.Where(x => x.Id == id).FirstOrDefault();
        }
    }
}

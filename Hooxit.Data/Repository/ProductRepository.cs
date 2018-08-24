using System.Collections.Generic;
using System.Linq;
using Data;
using Hooxit.Data.Contracts;
using Hooxit.Models;

namespace Hooxit.Data.Repository
{
    public class ProductRepository : IReadRepository<Product>, IRepository<Product>, IUpdateRepository<Product>
    {
        private readonly IHooxitDbContext dbContext;

        public ProductRepository(IHooxitDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void Add(Product entity)
        {
            dbContext.Products.Add(entity);
            Save();
        }

        public IList<Product> GetAll()
        {
            return dbContext.Products.ToList();
        }

        public Product GetById(int id)
        {
            return dbContext.Products.Where(x => x.ID == id).FirstOrDefault(); ;
        }

        public IList<Product> GetManyByIds(int[] ids)
        {
            return dbContext.Products.Where(x => ids.Contains(x.ID)).ToList();
        }

        public void Update(Product entity)
        {
            dbContext.Products.Update(entity);
            Save();
        }

        public void Save()
        {
            dbContext.SaveChanges();
        }
    }
}

using Hooxit.Models;
using Hooxit.Models.Users;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Data
{
    public class HooxitDbContext : IdentityDbContext<User>, IHooxitDbContext
    {
        public HooxitDbContext() : base() { }

        public HooxitDbContext(DbContextOptions<HooxitDbContext> options) : base(options) { }

        public DbSet<Candidate> Candidates { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Experience> Experience { get; set; }
        public DbSet<Country> Countries { get; set; }

        public override int SaveChanges()
        {
            return base.SaveChanges();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            builder.UseSqlServer(@"Server=.\SQLEXPRESS;Database=Hooxit;Trusted_Connection=True;Integrated Security=True");
            base.OnConfiguring(builder);
        }
    }
}

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
        public DbSet<Skill> Skills { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<PositionSkill> PositionSkill { get; set; }

        public override int SaveChanges()
        {
            return base.SaveChanges();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Manually create many to many relationship
            builder.Entity<PositionSkill>()
                .HasKey(bc => new { bc.SkillId, bc.PositionId });

            builder.Entity<PositionSkill>()
                .HasOne(bc => bc.Skill)
                .WithMany(b => b.PositionSkill)
                .HasForeignKey(bc => bc.SkillId);

            builder.Entity<PositionSkill>()
                .HasOne(bc => bc.Position)
                .WithMany(c => c.PositionSkill)
                .HasForeignKey(bc => bc.PositionId);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            builder.UseSqlServer(@"Server=.\SQLEXPRESS;Database=Hooxit;Trusted_Connection=True;Integrated Security=True");
            base.OnConfiguring(builder);
        }
    }
}

﻿using Hooxit.Models;
using Hooxit.Models.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Models;

namespace Data
{
    public interface IHooxitDbContext
    {
        DbSet<User> Users { get; set; }
        DbSet<Experience> Experience { get; set; }
        DbSet<Country> Countries { get; set; }
        DbSet<Candidate> Candidates { get; set; }
        DbSet<Company> Companies { get; set; }
        DbSet<Skill> Skills { get; set; }
        DbSet<Position> Positions { get; set; }
        DbSet<PositionSkill> PositionSkill { get; set; }
        DbSet<CandidateSkill> CandidateSkill { get; set; }
        DbSet<Product> Products { get; set; }
        DbSet<PositionCandidate> PositionsCandidates { get; set; }
        DbSet<CandidateInterest> CandidateInterests { get; set; }
        EntityEntry Entry(object entity);

        int SaveChanges();
    }
}

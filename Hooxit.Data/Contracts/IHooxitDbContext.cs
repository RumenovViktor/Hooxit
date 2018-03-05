﻿using Hooxit.Models;
using Hooxit.Models.Users;
using Microsoft.EntityFrameworkCore;
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

        int SaveChanges();
    }
}

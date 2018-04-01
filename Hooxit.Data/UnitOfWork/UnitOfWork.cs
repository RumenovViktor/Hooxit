﻿using Data;
using Hooxit.Data.Contracts;
using Hooxit.Data.Repository;
using Hooxit.Models;

namespace Hooxit.Data.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IHooxitDbContext dbContext;

        public UnitOfWork(IHooxitDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IRepository<Experience> BuildExperienceRepository()
        {
            return new ExperienceRepository(dbContext);
        }

        public IReadRepository<Country> BuildCountriesRepository()
        {
            return new CountriesRepository(dbContext);
        }

        public ICandidateRepository BuildCandidateRepository()
        {
            return new CandidateRepository(dbContext);
        }

        public ICompaniesRepository BuildCompaniesRepository()
        {
            return new CompaniesRepository(dbContext);
        }

        public IReadByNameRepository<Skill> BuildSkillsReadByNameRepository()
        {
            return new SkillsRepository(dbContext);
        }

        public IReadRepository<Skill> BuildSkillsReadRepository()
        {
            return new SkillsRepository(dbContext);
        }

        public IReadRepository<PositionSkill> BuildPositionSkillRepository()
        {
            return new PositionSkillRepository(dbContext);
        }

        public IRepository<Position> BuildPositionsRepository()
        {
            return new PositionsRepository(dbContext);
        }
    }
}

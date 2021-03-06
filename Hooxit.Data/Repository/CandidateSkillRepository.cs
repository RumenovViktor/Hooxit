﻿using System.Collections.Generic;
using System.Linq;
using Data;
using Hooxit.Data.Contracts;
using Hooxit.Models;
using Microsoft.EntityFrameworkCore;

namespace Hooxit.Data.Repository
{
    public class CandidateSkillRepository 
        : IRepository<CandidateSkill>, 
          IReadRepository<CandidateSkill>, 
          IDeleteRepository<CandidateSkill>
    {
        private readonly IHooxitDbContext dbContext;

        public CandidateSkillRepository(IHooxitDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void Add(CandidateSkill entity)
        {
            this.dbContext.CandidateSkill.Add(entity);
        }

        public IList<CandidateSkill> GetAll()
        {
            return this.dbContext.CandidateSkill.ToList();
        }

        public void Delete(CandidateSkill entity)
        {
            this.dbContext.Entry(entity).State = EntityState.Detached;
            this.dbContext.CandidateSkill.Remove(entity);
        }

        public CandidateSkill GetById(int id)
        {
            return this.dbContext.CandidateSkill.Where(x => id == x.CandidateId).FirstOrDefault();
        }

        public IList<CandidateSkill> GetManyByIds(int[] id)
        {
            return this.dbContext.CandidateSkill.Where(x => id.Contains(x.CandidateId)).ToList();
        }

        public void Save()
        {
            dbContext.SaveChanges();
        }
    }
}

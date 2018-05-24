using System;
using System.Collections.Generic;
using System.Linq;
using Hooxit.Data.Contracts;
using Hooxit.Data.Repository;
using Hooxit.Models;
using Hooxit.Presentation.Write;
using Hooxit.Services.Contracts;

namespace Hooxit.Services.Implementation.ApplicationServices
{
    public class SkillsService : ISkillsService
    {
        private readonly IUserRepository userRepository;
        private readonly ICandidateRepository candidateRepository;
        private readonly IRepository<CandidateSkill> candidateSkillRepository;

        public SkillsService(IUserRepository userRepository, IUnitOfWork unitOfWork)
        {
            this.userRepository = userRepository;
            this.candidateRepository = unitOfWork.BuildCandidateRepository();
            this.candidateSkillRepository = unitOfWork.BuildCandidateSkillRepository();
        }

        public bool ChangeSkills(ChangeSkills skills)
        {
            var user = userRepository.GetByName(UserInfo.UserName);
            var userInfo = candidateRepository.GetBydId(user.Result.Id);
            var candidateSkills = this.candidateSkillRepository.GetMany(new int[] { userInfo.Id });

            try
            {
                candidateSkills.ToList().ForEach(x => { candidateSkillRepository.Delete(x); });
                candidateSkillRepository.Save();

                var skillsASd = skills.Skills.Select(x => new CandidateSkill
                {
                    CandidateId = userInfo.Id,
                    SkillId = x
                }).ToList();

                skillsASd.ForEach(x => candidateSkillRepository.Add(x));
                candidateSkillRepository.Save();
            }
            catch (Exception e)
            {
                return false;
            }

            return true;
        }
    }
}

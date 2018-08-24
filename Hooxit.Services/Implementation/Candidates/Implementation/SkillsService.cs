using System;
using System.Linq;

using Hooxit.Data.Contracts;
using Hooxit.Data.Repository;
using Hooxit.Models;
using Hooxit.Presentation.Implemenation.Candidate.Write;
using Hooxit.Services.Candidates.Interfaces;

namespace Hooxit.Services.Implementation.ApplicationServices
{
    public class SkillsService : ISkillsService
    {
        private readonly IUserRepository userRepository;
        private readonly ICandidateRepository candidateRepository;
        private readonly IRepository<CandidateSkill> candidateSkillRepository;
        private readonly IReadRepository<CandidateSkill> candidateSkillReadRepository;
        private readonly IDeleteRepository<CandidateSkill> candidateSkillDeleteRepository;

        public SkillsService(IUserRepository userRepository, IUnitOfWork unitOfWork)
        {
            this.userRepository = userRepository;

            candidateRepository = unitOfWork.BuildCandidateRepository();
            candidateSkillReadRepository = unitOfWork.BuildCandidateSkillReadRepository();
            candidateSkillRepository = unitOfWork.BuildCandidateSkillRepository();
            candidateSkillDeleteRepository = unitOfWork.BuildCandidateSkillDeleteRepository();
        }

        public bool ChangeSkills(ChangeSkills skills)
        {
            var user = userRepository.GetByName(UserInfo.UserName);
            var userInfo = candidateRepository.GetById(user.Result.Id);
            var candidateSkills = this.candidateSkillReadRepository.GetManyByIds(new int[] { userInfo.Id });

            try
            {
                candidateSkills.ToList().ForEach(x => { candidateSkillDeleteRepository.Delete(x); });
                candidateSkillRepository.Save();

                var skillsASd = skills.Skills.Select(x => new CandidateSkill { CandidateId = userInfo.Id, SkillId = x }).ToList();

                skillsASd.ForEach(x => candidateSkillRepository.Add(x));
                candidateSkillRepository.Save();
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }
    }
}

using System.Collections.Generic;
using System.Linq;

using Hooxit.Data.Contracts;
using Hooxit.Data.Repository;
using Hooxit.Models;
using Hooxit.Presentation.Implemenation;
using Hooxit.Services.Company.Interfaces;

namespace Hooxit.Services.Company.Implemenation
{
    public class PositionSkillRelationManager : IPositionSkillRelationManager
    {
        private readonly IReadRepository<PositionSkill> positionSkillRepository;
        private readonly IRepository<CandidateSkill> candidateSkillRepository;
        private readonly IReadRepository<Skill> skillsRepository;
        private readonly ICandidateRepository candidateRepository;
        private readonly IUserRepository userRepository;

        public PositionSkillRelationManager(IUnitOfWork unitOfWork, IUserRepository userRepository)
        {
            positionSkillRepository = unitOfWork.BuildPositionSkillRepository();
            candidateSkillRepository = unitOfWork.BuildCandidateSkillRepository();
            candidateRepository = unitOfWork.BuildCandidateRepository();
            skillsRepository = unitOfWork.BuildSkillsReadRepository();

            this.userRepository = userRepository;
        }

        public RelationRead<PositionSkillsRelation> GetRelation(string candidateUserName, int positionId)
        {
            var user = userRepository.GetByName(candidateUserName);
            var candidate = candidateRepository.GetBydId(user.Result.Id);
            var candidateSkills = candidateSkillRepository.GetMany(new int[] { candidate.Id });
            var positionSkills = positionSkillRepository.GetManyById(new int[] { positionId });
            var skillsWithNames = skillsRepository.GetManyById(positionSkills.Select(x => x.SkillId).ToArray());
            var skillsRelation = new List<PositionSkillsRelation>();

            positionSkills.ToList().ForEach(x =>
            {
                var name = skillsWithNames.Where(t => t.ID == x.SkillId).FirstOrDefault().SkillName;

                if (candidateSkills.Any(c => x.SkillId == c.SkillId))
                { 
                    skillsRelation.Add(new PositionSkillsRelation
                    {
                        CandidateSkill = name,
                        PositionSkill = name,
                        IsMatch = true,
                    });
                }
                else
                {
                    skillsRelation.Add(new PositionSkillsRelation
                    {
                        CandidateSkill = name,
                        PositionSkill = name,
                        IsMatch = false,
                    });
                }
            });

            return new RelationRead<PositionSkillsRelation>
            {
                Name = candidateUserName,
                SkillsRelation = skillsRelation
            };
        }
    }
}

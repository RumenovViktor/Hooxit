using System;
using System.Threading.Tasks;

using Hooxit.Data.Contracts;
using Hooxit.Data.Repository;
using Hooxit.Presentation.Implemenation.Candidate.Write;
using Hooxit.Services.Candidates.Interfaces;

namespace Hooxit.Services.Candidates.UserInfoHandlers
{
    public class ChangeCurrentPositionHandler : IUserPersonalInfoHandler<ChangeCurrentPosition>
    {
        private readonly IUserRepository userRepository;
        private readonly ICandidateRepository candidateRepository;

        public ChangeCurrentPositionHandler(IUserRepository userRepository, IUnitOfWork unitOfWork)
        {
            this.userRepository = userRepository;
            this.candidateRepository = unitOfWork.BuildCandidateRepository();
        }

        public async Task<ChangeCurrentPosition> Handle(ChangeCurrentPosition command) // Show notification if the last position is not the updated one
        {
            var user = await userRepository.GetByName(UserInfo.UserName);
            var userInfo = candidateRepository.GetBydId(user.Id);

            userInfo.CurrentPosition = command.Position;

            var updatedUser = await userRepository.Update(user);
            candidateRepository.Save();

            if (updatedUser.Succeeded)
            {
                return command;
            }
            else
            {
                throw new ArgumentException("Something went wrong", updatedUser.Errors.ToString());
            }
        }
    }
}

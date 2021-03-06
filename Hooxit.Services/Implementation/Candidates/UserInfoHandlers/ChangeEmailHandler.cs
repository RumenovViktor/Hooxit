﻿using System;
using System.Threading.Tasks;

using Hooxit.Data.Repository;
using Hooxit.Presentation.Implemenation.Candidate.Write;
using Hooxit.Services.Candidates.Interfaces;

namespace Hooxit.Services.Candidates.UserInfoHandlers
{
    public class ChangeEmailHandler : IUserPersonalInfoHandler<ChangeEmail>
    {
        private readonly IUserRepository userRepository;

        public ChangeEmailHandler(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public async Task<ChangeEmail> Handle(ChangeEmail command)
        {
            var user = await userRepository.GetByName(UserInfo.UserName);
            user.Email = command.Email;

            var updatedUser = await userRepository.Update(user);

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

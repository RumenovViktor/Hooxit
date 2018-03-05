using Hooxit.Data.Repository;
using Hooxit.Presentation.Write;
using Hooxit.Services.Contracts;
using Microsoft.AspNetCore.Identity;
using Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hooxit.Services.Implementation.Profile
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

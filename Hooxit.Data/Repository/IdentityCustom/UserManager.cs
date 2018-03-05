using Microsoft.AspNetCore.Identity;
using Models;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Hooxit.Data.Repository.IdentityCustom
{
    public class UserManager : UserManager<User>
    {
        public UserManager(IUserStore<User> store, IOptions<IdentityOptions> optionsAccessor, 
            IPasswordHasher<User> passwordHasher, IEnumerable<IUserValidator<User>> userValidators, 
            IEnumerable<IPasswordValidator<User>> passwordValidators, ILookupNormalizer keyNormalizer, 
            IdentityErrorDescriber errors, IServiceProvider services, ILogger<UserManager<User>> logger) 
                : base(store, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors, services, logger)
        {
        }
    }
}

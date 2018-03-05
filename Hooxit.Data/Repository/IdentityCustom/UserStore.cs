using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Hooxit.Data.Repository.IdentityCustom
{
    public class UserStore : UserStore<User>
    {
        public UserStore(DbContext context, IdentityErrorDescriber describer = null)
            : base(context, describer)
        {
        }

        public override Task<User> FindByNameAsync(string userName, CancellationToken cancellationToken = default(CancellationToken))
        {
            return null;//Users.Include(x => x.Experience).FirstOrDefaultAsync(x => x.UserName == userName);
        }
    }
}

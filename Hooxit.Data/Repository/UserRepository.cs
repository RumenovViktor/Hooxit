using System.Threading.Tasks;
using Models;
using Microsoft.AspNetCore.Identity;

namespace Hooxit.Data.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<User> userManager;

        public UserRepository(UserManager<User> userManager)
        {
            this.userManager = userManager;
        }

        public async Task<User> Get(string id)
        {
            var result = await userManager.FindByIdAsync(id);
            return result;
        }

        public async Task<User> GetByName(string name)
        {
            var result = await userManager.FindByNameAsync(name);
            return result;
        }

        public async Task<bool> Create(User user, string password)
        {
            var result = await userManager.CreateAsync(user, password);

            if (result.Succeeded)
            {
                return true;
            }

            return false;
        }

        public async Task<IdentityResult> Update(User user)
        {
            return await userManager.UpdateAsync(user);
        }
    }
}

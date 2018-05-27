using Microsoft.AspNetCore.Identity;
using Models;
using System.Threading.Tasks;

namespace Hooxit.Data.Repository
{
    public interface IUserRepository
    {
        Task<User> Get(string id);

        Task<User> GetByName(string name);

        Task<bool> Create(User user, string password);

        Task<IdentityResult> Update(User user);
    }
}

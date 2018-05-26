using Models;
using System.Threading.Tasks;

namespace Hooxit.Services.Authentication.Interfaces
{
    public interface IAuthenticationProvider
    {
        Task SignIn(User user, bool isPersistent = false);

        Task<bool> PasswordSignIn(string userName, string password, bool isPersistent, bool shouldLockout);

        Task SignOut();

        Task AddClaim(User user, string claimType, string claimValue);
    }
}

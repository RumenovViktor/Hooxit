using System.Threading.Tasks;
using Hooxit.Services.Contracts;
using Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace Hooxit.Services.Implementation
{
    public class AuthenticationProvider : IAuthenticationProvider
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;

        public AuthenticationProvider(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        public async Task SignIn(User user, bool isPersistent = false)
        {
            await signInManager.SignInAsync(user, isPersistent);
        }

        public async Task<bool> PasswordSignIn(string userName, string password, bool isPersistent, bool shouldLockout)
        {
            var result = await signInManager.PasswordSignInAsync(userName, password, isPersistent, shouldLockout);

            if (result.Succeeded)
            {
                return true;
            }

            return false;
        }

        public async Task SignOut()
        {
            await signInManager.SignOutAsync();
        }

        public async Task AddClaim(User user, string claimType, string claimValue)
        {
            await userManager.AddClaimAsync(user, new Claim(claimType, claimValue));
        }
    }
}

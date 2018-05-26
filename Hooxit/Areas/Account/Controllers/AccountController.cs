using Hooxit.Data.Repository;
using Hooxit.Presentation;
using Hooxit.Services.Authentication.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Models;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Hooxit.Account.Controllers
{
    [Area("Account")]
    public class AccountController : BaseController
    {
        private readonly ILogger logger;
        private readonly IUserRepository userRepository;
        private readonly IAuthenticationProvider authenticationProvider;

        public AccountController(IUserRepository userRepository, IAuthenticationProvider authenticationProvider, ILoggerFactory loggerFactory)
        {
            this.userRepository = userRepository;
            this.authenticationProvider = authenticationProvider;
            this.logger = loggerFactory.CreateLogger<AccountController>();
        }

        [HttpGet]
        [Route("")]
        [Route("Login")]
        public IActionResult Index()
        {
            return View(new Login());
        }

        public async Task<IActionResult> Logout()
        {
            await authenticationProvider.SignOut();

            logger.LogInformation("User logged out.");

            return RedirectToAction(nameof(AccountController.Index), "Account");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("")]
        public async Task<IActionResult> Login(Login userLogin)
        {
            if (!ModelState.IsValid)
            {
                return View(userLogin);
            }

            var isSuccess = await authenticationProvider.PasswordSignIn(userLogin.UserName, userLogin.Password, true, false);

            if (isSuccess)
            {
                logger.LogInformation(1, "User logged in.");

                var isCompany = User.HasClaim("CompanyRole", "Company");
                var areaName = isCompany ? "Company" : "Candidate";

                return RedirectToAction("Index", "Dashboard", new { area = areaName });
            }
            
            return View(userLogin);
        }

        [HttpGet]
        [Route("Register")]
        public IActionResult Register()
        {
            return View(new Register());
        }

        [HttpPost]
        [Route("Register")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(Register userRegistration)
        {
            if (!ModelState.IsValid)
            {
                return View(userRegistration);
            }

            var user = new User { UserName = userRegistration.UserName, Email = userRegistration.Email };
            var isSuccess = await userRepository.Create(user, userRegistration.Password);

            await authenticationProvider.AddClaim(user, ClaimTypes.Name, user.UserName);

            if (isSuccess)
            {
                logger.LogInformation($"User with username: {user.UserName} just registered");
                await authenticationProvider.SignIn(user);

                return RedirectToAction(nameof(UserTypeController.ChooseUserType), "UserType", new { area = "Account" });
            }

            return View(userRegistration);
        }
    }
}

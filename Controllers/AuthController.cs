using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyForum.Core.Filters;
using MyForum.Core.Services.Interface;
using MyForum.ViewModels;
using MyForum.Core.Extensions;

namespace MyForum.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthService authService;
        private readonly ITokenClaimsManager tokenClaimsManager;
        private readonly IResetPasswordService resetPasswordService;

        public AuthController(IAuthService authService, ITokenClaimsManager tokenClaimsManager,
            IResetPasswordService resetPasswordService)
        {
            this.authService = authService;
            this.tokenClaimsManager = tokenClaimsManager;
            this.resetPasswordService = resetPasswordService;
        }

        [HttpGet]
        [ServiceFilter(typeof(OnlyAnonymousFilter))]
        public IActionResult Login()
            => View(new LoginViewModel().WithAlert());

        [HttpGet]
        [ServiceFilter(typeof(OnlyAnonymousFilter))]
        public IActionResult Register()
            => View(new RegisterViewModel().WithAlert());

        [HttpGet]
        [ServiceFilter(typeof(OnlyAnonymousFilter))]
        public IActionResult ResetPassword()
            => View(new ResetPasswordViewModel().WithAlert());

        [HttpPost]
        [ServiceFilter(typeof(OnlyAnonymousFilter))]
        public async Task<IActionResult> Login(LoginViewModel viewModel)
        {
            var loggedUser = await authService.Login(viewModel.Username, viewModel.Password);

            if (loggedUser == null) return View(viewModel.WithError());
            await tokenClaimsManager.LoginWithClaims(loggedUser);

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [ServiceFilter(typeof(OnlyAnonymousFilter))]
        public async Task<IActionResult> Register(RegisterViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return View(viewModel);

            var registeredUser = await authService.Register(viewModel.Username, viewModel.Email, viewModel.Password);

            return registeredUser != null
                ? RedirectToAction("Login")
                    .PushAlert("Registered successfully. Check your email to confirm your account.")
                : View(viewModel.WithError());
        }

        [HttpGet]
        [ServiceFilter(typeof(OnlyAnonymousFilter))]
        public async Task<IActionResult> ConfirmAccount([FromQuery] ConfirmAccountViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return this.ErrorPage();

            return await authService.ConfirmAccount(viewModel.Email, viewModel.Token)
                ? (IActionResult) RedirectToAction("Login").PushAlert("Account was confirmed")
                : this.ErrorPage();
        }

        [HttpPost]
        [ServiceFilter(typeof(OnlyAnonymousFilter))]
        public async Task<IActionResult> SendResetPassword(ResetPasswordViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return View("ResetPassword", viewModel);

            await resetPasswordService.SendResetPasswordCallback(viewModel.Email, viewModel.NewPassword);

            return RedirectToAction("ResetPassword").PushAlert($"Reset token has been sent to: {viewModel.Email}");
        }

        [HttpGet]
        [ServiceFilter(typeof(OnlyAnonymousFilter))]
        public async Task<IActionResult> ConfirmResetPassword([FromQuery] ResetPasswordCallbackViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return this.ErrorPage();

            return await resetPasswordService.ResetPassword(viewModel.Email, viewModel.NewPassword, viewModel.Token)
                ? (IActionResult) RedirectToAction("Login").PushAlert("Password has been changed")
                : this.ErrorPage();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await authService.Logout();

            return RedirectToAction("Login");
        }
    }
}
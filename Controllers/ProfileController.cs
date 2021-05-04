using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MyForum.Core.Extensions;
using MyForum.Core.Services.Interface;
using MyForum.Models.Domain.Auth;
using MyForum.ViewModels;

namespace MyForum.Controllers
{
    public class ProfileController : Controller
    {
        private static ProfileViewModel profileViewModel;
        private readonly IProfileService profileService;
        private readonly IMapper mapper;
        public ProfileController(IProfileService profileService, IMapper mapper)
        {
            this.profileService = profileService;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var user = await profileService.GetCurrentUser();
            if (user == null)
                return this.ErrorPage();

            profileViewModel = (ProfileViewModel)mapper.Map<User, ProfileViewModel>(user, profileViewModel).WithAlert();

            return View(profileViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return View("Index", profileViewModel);

            return RedirectToAction("Index").PushAlert(
                await profileService.ChangePassword(viewModel.OldPassword, viewModel.NewPassword)
                ? "Password changed successfully"
                : "Cannot change password");
        }

        [HttpPost]
        public async Task<IActionResult> ChangeEmail(ChangeEmailViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return View("Index", profileViewModel);

            return RedirectToAction("Index").PushAlert(
                await profileService.ChangeEmail(viewModel.NewEmail)
                ? "Email has been changed"
                : "Cannot change email");
        }
    }
}
using Microsoft.AspNetCore.Mvc;

namespace MyForum.ViewModels
{
    public class ProfileViewModel : BaseViewModel
    {
        public string Email { get; set; }
        public string Username { get; set; }
        public string AvatarUrl { get; set; }

        public ProfileViewModel()
        {
            Title = "Profile";
        }
    }
}
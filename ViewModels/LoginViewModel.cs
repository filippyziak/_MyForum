using MyForum.Core.Services;

namespace MyForum.ViewModels
{
    public class LoginViewModel : ErrorBaseViewModel
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public LoginViewModel()
        {
            Title = "Login";
        }

        public void Dispose()
        {
            Alertify.Clear();
        }
    }
}
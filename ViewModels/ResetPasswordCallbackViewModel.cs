using MyForum.Core.Validators;

namespace MyForum.ViewModels
{
    public class ResetPasswordCallbackViewModel : ErrorBaseViewModel
    {
        [RequiredValidator]
        public string Email { get; set; }

        [RequiredValidator]
        public string Token { get; set; }

        [RequiredValidator]
        public string NewPassword { get; set; }
        public ResetPasswordCallbackViewModel()
        {
            Title = "ResetPassword";
        }
    }
}
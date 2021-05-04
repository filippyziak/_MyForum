using System.ComponentModel.DataAnnotations;
using MyForum.Core.Helpers;
using MyForum.Core.Validators;

namespace MyForum.ViewModels
{
    public class ResetPasswordViewModel : ErrorBaseViewModel
    {
        [RequiredValidator]
        [EmailAddress]
        public string Email { get; set; }

        [RequiredValidator]
        [StringLengthValidator(Constants.MaxPasswordLength, Constants.MinPasswordLength)]
        [DataType(DataType.Password)]
        [WhitespacesNotAllowedValidator]
        public string NewPassword { get; set; }

        public ResetPasswordViewModel()
        {
            Title = "ResetPassword";
        }
    }
}
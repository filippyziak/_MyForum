using System.ComponentModel.DataAnnotations;
using MyForum.Core.Helpers;
using MyForum.Core.Validators;

namespace MyForum.ViewModels
{
    public class RegisterViewModel : ErrorBaseViewModel
    {
        [RequiredValidator]
        [EmailAddress]
        public string Email { get; set; }

        [RequiredValidator]
        [StringLengthValidator(Constants.MaxUsernameLength, Constants.MinUsernameLength)]
        [WhitespacesNotAllowedValidator]
        public string Username { get; set; }

        [RequiredValidator]
        [StringLengthValidator(Constants.MaxPasswordLength, Constants.MinPasswordLength)]
        [DataType(DataType.Password)]
        [WhitespacesNotAllowedValidator]
        public string Password { get; set; }

        public RegisterViewModel()
        {
            Title = "Register";
        }
    }
}
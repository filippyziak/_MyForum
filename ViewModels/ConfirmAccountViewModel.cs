using System.ComponentModel.DataAnnotations;
using MyForum.Core.Validators;

namespace MyForum.ViewModels
{
    public class ConfirmAccountViewModel : BaseViewModel
    {
        [RequiredValidator]
        [EmailAddress]
        public string Email { get; set; }
        [RequiredValidator]
        public string Token { get; set; }

        public ConfirmAccountViewModel()
        {
            Title = "Confirm account";
        }
    }
}
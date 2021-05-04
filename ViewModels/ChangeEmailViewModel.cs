using System.ComponentModel.DataAnnotations;
using MyForum.Core.Validators;

namespace MyForum.ViewModels
{
    public class ChangeEmailViewModel
    {
        [RequiredValidator]
        [EmailAddress]
        public string NewEmail { get; set; }
    }
}
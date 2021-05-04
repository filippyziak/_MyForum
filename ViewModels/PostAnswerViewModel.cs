using System.ComponentModel.DataAnnotations;
using MyForum.Core.Helpers;
using MyForum.Core.Validators;

namespace MyForum.ViewModels
{
    public class CreateAnswerViewModel
    {
        [Required]
        [StringLengthValidator(Constants.MaxPostContentLength, Constants.MinPostContentLength)]
        public string Content { get; set; }

        [Required]
        public string PostId { get; set; }

    }
}
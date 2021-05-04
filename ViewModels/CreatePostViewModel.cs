using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MyForum.Core.Helpers;
using MyForum.Core.Validators;
using MyForum.Models.Domain.Post;

namespace MyForum.ViewModels
{
    public class CreatePostViewModel : ErrorBaseViewModel
    {
        [Required]
        [StringLengthValidator(Constants.MaxPostTitleLength, Constants.MinPostTitleLength)]
        public string PostTitle { get; set; }

        [Required]
        [StringLengthValidator(Constants.MaxPostContentLength, Constants.MinPostContentLength)]
        public string Content { get; set; }

        [Required]
        public string CategoryId { get; set; }

        public IEnumerable<Category> CategoriesToSelect { get; set; }

        public CreatePostViewModel()
        {
            Title = "CreatePost";
        }
    }
}
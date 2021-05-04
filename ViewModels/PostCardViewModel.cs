using System;
using System.Collections.Generic;
using MyForum.Core.Extensions;
using MyForum.Core.Helpers;
using MyForum.Models.Domain.Post;
using MyForum.Models.Helpers.Pagination;

namespace MyForum.ViewModels
{
    public class PostCardViewModel : BaseViewModel
    {
        public string PostTitle { get; set; }
        public string Content { get; set; }
        public string UserId { get; set; }
        public string Username { get; set; }
        public string PostId { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
        public List<Answer> Answers { get; set; }

        public PostCardViewModel()
        {
            Title = "XD";
        }
    }
}
using System.Collections.Generic;
using MyForum.Core.Enums;
using MyForum.Core.Extensions;
using MyForum.Core.Helpers;
using MyForum.Core.Params;
using MyForum.Models.Domain.Post;
using MyForum.Models.Helpers.Pagination;

namespace MyForum.ViewModels
{
    public class CategoryViewModel : ErrorBaseViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public PagedList<Post> Posts { get; set; }

        public string TitleFilter { get; set; }
        public string Username { get; set; }
        public string CurrentUserId { get; set; }
        public PostSortType SortType { get; set; }

        public CategoryViewModel FilterPosts(PagedList<Post> posts, string name)
        {
            Name = name;
            Title = name;
            Posts = posts;
            return this;
        }

        public CategoryViewModel MapPosts(ICollection<Post> posts, string currentUserId)
        {
            CurrentUserId = currentUserId;
            Posts = posts.ToPagedList<Post>(1, Constants.DefaultPageSize);
            return this;
        }
    }
}
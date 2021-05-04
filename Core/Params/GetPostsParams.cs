using MyForum.Core.Enums;
using MyForum.Models.Domain.Post;

namespace MyForum.Core.Params
{
    public class GetPostsParams : FilterParams
    {
        public string Title { get; private set; }
        public string CategoryId { get; private set; }
        public string Username { get; private set; }
        public PostSortType SortType { get; private set; } = PostSortType.UpdatedDescending;

        public static GetPostsParams Create(string title, string categoryId, string username, PostSortType sortType)
        => new GetPostsParams
        {
            Title = title,
            CategoryId = categoryId,
            Username = username,
            SortType = sortType
        };
    }
}
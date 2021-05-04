using System.Threading.Tasks;
using MyForum.Core.Params;
using MyForum.Models.Domain.Post;
using MyForum.Models.Helpers.Pagination;

namespace MyForum.Core.Services.Interface
{
    public interface IPostService
    {
        Task<Post> GetPost(string postId);
        Task<Post> CreatePost(string title, string content, string categoryId);
        Task<bool> DeletePost(string postId);
        Task<bool> UpdatePost(string postId, string title, string content, string categoryId);
        Task<PagedList<Post>> GetPosts(GetPostsParams postsParams);
    }
}
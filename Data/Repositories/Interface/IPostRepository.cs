using System.Threading.Tasks;
using MyForum.Models.Domain.Post;

namespace MyForum.Data.Repositories.Interface
{
    public interface IPostRepository : IRepository<Post>
    {
        Task<Post> GetPostWithAnswers(string id);
    }
}
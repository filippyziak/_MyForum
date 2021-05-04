using System.Threading.Tasks;
using MyForum.Models.Domain.Auth;

namespace MyForum.Data.Repositories.Interface
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> GetUserWithToken(string email);
        Task<User> GetUserWithPosts(string userId);
    }
}
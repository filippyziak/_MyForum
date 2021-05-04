using System.Threading.Tasks;
using MyForum.Models.Domain.Auth;
namespace MyForum.Core.Services.Interface
{
    public interface IAuthService
    {
        Task<User> Login(string username, string password);
        Task<User> Register(string username, string email, string password);
        Task<bool> ConfirmAccount(string email, string token);
        Task Logout();
        Task<bool> EmailExists(string email);
        Task<bool> UsernameExists(string username);
    }
}
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using MyForum.Models.Domain.Auth;

namespace MyForum.Core.Services.Interface
{
    public interface IProfileService
    {
        Task<User> GetCurrentUser();
        Task<bool> ChangePassword(string oldPassword, string newPassword);
        Task<bool> ChangeEmail(string newEmail);
        Task<bool> SetAvatar(IFormFile photo);
        Task<bool> DeleteAvatar();
    }
}
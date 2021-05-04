using System.Threading.Tasks;

namespace MyForum.Core.Services.Interface
{
    public interface IResetPasswordService
    {
        Task<bool> SendResetPasswordCallback(string email, string newPassword);
        Task<bool> ResetPassword(string email, string newPassword, string token);
    }
}
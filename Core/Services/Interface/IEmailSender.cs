using System.Threading.Tasks;
using MyForum.Models.Helpers.Email;

namespace MyForum.Core.Services.Interface
{
    public interface IEmailSender
    {
        Task<bool> Send(EmailMessage emailMessage);
    }
}
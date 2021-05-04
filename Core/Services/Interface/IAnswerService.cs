using System.Threading.Tasks;
using MyForum.Core.Params;
using MyForum.Models.Domain.Post;
using MyForum.Models.Helpers.Pagination;

namespace MyForum.Core.Services.Interface
{
    public interface IAnswerService
    {
        Task<Answer> CreateAnswer(string content, string postId);
        Task<bool> DeleteAnswer(string id);
    }
}
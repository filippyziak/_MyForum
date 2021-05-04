using System.Threading.Tasks;
using MyForum.Core.Params;
using MyForum.Core.Services.Interface;
using MyForum.Data;
using MyForum.Models.Domain.Post;
using MyForum.Models.Helpers.Pagination;

namespace MyForum.Core.Services
{
    public class AnswerService : IAnswerService
    {
        private readonly IProfileService profileService;
        private readonly IDatabase database;
        public AnswerService(IProfileService profileService, IDatabase database)
        {
            this.profileService = profileService;
            this.database = database;
        }
        public async Task<Answer> CreateAnswer(string content, string postId)
        {
            var currentUser = await profileService.GetCurrentUser();
            var post = await database.PostRepository.Get(postId);

            if (currentUser == null || post == null)
                return null;

            var answer = Answer.Create(content, currentUser.Username);

            currentUser.Answers.Add(answer);
            post.Answers.Add(answer);
            post.UpdateDate();

            return await database.Complete() ? answer : null;
        }

        public async Task<bool> DeleteAnswer(string id)
        {
            var currentUser = await profileService.GetCurrentUser();

            if (currentUser == null)
                return false;

            var answer = await database.AnswerRepository.Find(a => a.Id == id && a.UserId == currentUser.Id);

            if (answer == null)
                return false;

            database.AnswerRepository.Delete(answer);

            return await database.Complete();
        }
    }
}
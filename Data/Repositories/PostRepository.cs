using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyForum.Data.Repositories.Interface;
using MyForum.Models.Domain.Post;

namespace MyForum.Data.Repositories
{
    public class PostRepository : Repository<Post>, IPostRepository
    {
        public PostRepository(DataContext context) : base(context) { }
        public async Task<Post> GetPostWithAnswers(string id)
         => await context.Posts
        .Include(p => p.Answers)
        .FirstOrDefaultAsync(p => p.Id == id);
    }
}
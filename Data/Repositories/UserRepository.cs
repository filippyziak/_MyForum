using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyForum.Data.Repositories.Interface;
using MyForum.Models.Domain.Auth;

namespace MyForum.Data.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(DataContext context) : base(context) { }


        public async Task<User> GetUserWithToken(string email)
         => await context.Users
            .Include(u => u.Tokens)
            .FirstOrDefaultAsync(u => u.Email.ToLower() == email.ToLower());
        public async Task<User> GetUserWithPosts(string userId)
            => await context.Users
            .Include(u => u.Posts)
            .FirstOrDefaultAsync(u => u.Id == userId);

    }
}
using System.Threading.Tasks;
using MyForum.Data.Repositories;
using MyForum.Data.Repositories.Interface;
using MyForum.Models.Domain.Auth;
using MyForum.Models.Domain.Post;

namespace MyForum.Data
{
    public class Database : IDatabase
    {
        private readonly DataContext context;

        public Database(DataContext context)
        {
            this.context = context;
        }

        #region repositories
        private IUserRepository userRepository;
        public IUserRepository UserRepository => userRepository ?? new UserRepository(context);

        private IPostRepository postRepository;
        public IPostRepository PostRepository => postRepository ?? new PostRepository(context);
        private ICategoryRepository categoryRepository;
        public ICategoryRepository CategoryRepository => categoryRepository ?? new CategoryRepository(context);

        private IRepository<Answer> answerRepository;
        public IRepository<Answer> AnswerRepository => answerRepository ?? new Repository<Answer>(context);
        #endregion
        public async Task<bool> Complete()
            => await context.SaveChangesAsync() > 0;

        public bool HasChanges()
            => context.ChangeTracker.HasChanges();

        public void Dispose()
        {
            context.Dispose();
        }
    }
}
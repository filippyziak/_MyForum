using System;
using System.Threading.Tasks;
using MyForum.Data.Repositories.Interface;
using MyForum.Models.Domain.Post;

namespace MyForum.Data
{
    public interface IDatabase : IDisposable
    {
        IUserRepository UserRepository { get; }
        IPostRepository PostRepository { get; }
        ICategoryRepository CategoryRepository { get; }
        IRepository<Answer> AnswerRepository { get; }
        bool HasChanges();
        Task<bool> Complete();
    }
}
using System.Collections.Generic;
using System.Threading.Tasks;
using MyForum.Models.Domain.Post;

namespace MyForum.Data.Repositories.Interface
{
    public interface ICategoryRepository : IRepository<Category>
    {
        Task<Category> GetCategoryWithPosts(string id);
    }
}
using System.Collections.Generic;
using System.Threading.Tasks;
using MyForum.Models.Domain.Post;

namespace MyForum.Core.Services.Interface
{
    public interface ICategoryService
    {
        Task<bool> CreateCategory(string name);
        Task<Category> GetCategory(string categoryId);
        Task<IEnumerable<Category>> GetCategories();
    }
}
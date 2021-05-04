using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyForum.Data.Repositories.Interface;
using MyForum.Models.Domain.Post;

namespace MyForum.Data.Repositories
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(DataContext context) : base(context) { }


        public async Task<Category> GetCategoryWithPosts(string id)
        => await context.Categories
        .Include(c => c.Posts)
        .FirstOrDefaultAsync(c => c.Id == id);
    }
}
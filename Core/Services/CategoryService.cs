using System.Collections.Generic;
using System.Threading.Tasks;
using MyForum.Core.Services.Interface;
using MyForum.Data;
using MyForum.Models.Domain.Post;

namespace MyForum.Core.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IDatabase database;
        public CategoryService(IDatabase database)
        {
            this.database = database;
        }
        public async Task<bool> CreateCategory(string categoryName)
        {
            if (await CategoryExists(categoryName))
                return false;

            database.CategoryRepository.Add(Category.Create(categoryName));

            return await database.Complete();
        }

        public async Task<IEnumerable<Category>> GetCategories()
        => await database.CategoryRepository.Fetch();

        public async Task<Category> GetCategory(string categoryId)
        => await database.CategoryRepository.GetCategoryWithPosts(categoryId);

        private async Task<bool> CategoryExists(string categoryName)
        => await database.CategoryRepository.Find(c => c.Name.ToLower() == categoryName.ToLower()) != null ? true : false;
    }
}
using System.Collections.Generic;
using MyForum.Core.Services.Interface;
using MyForum.Models.Domain.Post;
using Newtonsoft.Json;

namespace MyForum.Core.Services
{
    public class DatabaseManager : IDatabaseManager
    {
        private readonly ICategoryService categoryService;
        private const string CategoriesFilePath = "wwwroot/files/data/categories.json";
        public DatabaseManager(ICategoryService categoryService)
        {
            this.categoryService = categoryService;

        }

        public void Seed()
        {
            InsertCategories();
        }

        private void InsertCategories()
        {
            var categoriesData = System.IO.File.ReadAllText(CategoriesFilePath);
            var categories = JsonConvert.DeserializeObject<List<Category>>(categoriesData);

            foreach (var category in categories)
                categoryService.CreateCategory(category.Name).Wait();
        }
    }
}
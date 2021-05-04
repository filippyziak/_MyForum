using System.Collections.Generic;
using MyForum.Models.Domain.Post;

namespace MyForum.ViewModels
{
    public class CategoriesViewModel : ErrorBaseViewModel
    {
        public IEnumerable<CategoryViewModel> Categories { get; set; }
        public CategoriesViewModel()
        {
            Title = "Categories";
        }
    }
}
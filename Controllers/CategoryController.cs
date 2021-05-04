using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MyForum.Core.Extensions;
using MyForum.Core.Params;
using MyForum.Core.Services.Interface;
using MyForum.Models.Domain.Post;
using MyForum.ViewModels;

namespace MyForum.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService categoryService;
        private readonly IMapper mapper;
        private readonly IPostService postService;
        private readonly IProfileService profileService;

        public CategoryController(ICategoryService categoryService, IMapper mapper, IPostService postService,
        IProfileService profileService)
        {
            this.categoryService = categoryService;
            this.mapper = mapper;
            this.postService = postService;
            this.profileService = profileService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var categories = await categoryService.GetCategories();
            if (categories == null)
                return this.ErrorPage();

            var mappedCategories = mapper.Map<IEnumerable<Category>, IEnumerable<CategoryViewModel>>(categories);

            return View(new CategoriesViewModel { Categories = mappedCategories });
        }

        [HttpGet]
        public async Task<IActionResult> Details(string id)
        {
            var category = await categoryService.GetCategory(id);
            if (category == null)
                return this.ErrorPage();

            var currentUser = await profileService.GetCurrentUser();

            var model = mapper.Map<Category, CategoryViewModel>(category);
            model.MapPosts(category.Posts.OrderByDescending(p => p.DateUpdated).ToList(), currentUser.Id);
            return View(model.WithAlert());
        }

        [HttpPost]
        public async Task<IActionResult> Details(CategoryViewModel viewModel, [FromQuery] int pageNumber = 1)
        {
            return View(viewModel.FilterPosts(await postService.GetPosts(GetPostsParams.Create
            (
                viewModel.TitleFilter,
                viewModel.Id,
                viewModel.Username,
                viewModel.SortType
            ).CurrentPage(pageNumber) as GetPostsParams), viewModel.Name));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id, string categoryId)
        => await postService.DeletePost(id)
        ? RedirectToAction("Details", new { id = categoryId }).PushAlert("Post successfully deleted")
        : this.ErrorPage();
    }
}
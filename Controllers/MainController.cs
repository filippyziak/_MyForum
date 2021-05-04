using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyForum.Core.Extensions;
using MyForum.Core.Services.Interface;
using MyForum.ViewModels;

namespace MyForum.Controllers
{
    public class MainController : Controller
    {
        private readonly IPostService postService;
        private readonly ICategoryService categoryService;
        private readonly MainViewModel mainViewModel;

        public MainController(IPostService postService, ICategoryService categoryService)
        {
            this.postService = postService;
            this.categoryService = categoryService;
        }

        [HttpGet]
        public IActionResult Index()
            => View(new MainViewModel().WithAlert());


        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var categories = await categoryService.GetCategories();
            return View(new CreatePostViewModel { CategoriesToSelect = categories }.WithAlert());
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreatePostViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return RedirectToAction("Create").PushAlert("Post is valid, max title is 30 max content is 255", Core.Enums.AlertType.Error);

            var createdPost = await postService.CreatePost(viewModel.PostTitle, viewModel.Content, viewModel.CategoryId);

            if (createdPost == null)
                return RedirectToAction("Create").PushAlert("Error occured when creating a post");

            return RedirectToAction("Create").PushAlert("Post created successfully");
        }
    }
}
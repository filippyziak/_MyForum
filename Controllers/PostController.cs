using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MyForum.Core.Extensions;
using MyForum.Core.Services.Interface;
using MyForum.Data;
using MyForum.Models.Domain.Post;
using MyForum.Models.Helpers.Pagination;
using MyForum.ViewModels;

namespace MyForum.Controllers
{
    public class PostController : Controller
    {
        private readonly IMapper mapper;
        private readonly IDatabase database;
        private readonly IProfileService profileService;
        private readonly IAnswerService answerService;
        private readonly IPostService postService;

        public PostController(IMapper mapper, IDatabase database, IProfileService profileService,
        IAnswerService answerService, IPostService postService)
        {
            this.mapper = mapper;
            this.database = database;
            this.profileService = profileService;
            this.answerService = answerService;
            this.postService = postService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string id)
        {
            var post = await database.PostRepository.GetPostWithAnswers(id);
            if (post == null)
                return this.ErrorPage();

            var user = await profileService.GetCurrentUser();

            var model = mapper.Map<Post, PostCardViewModel>(post);
            return View(model.WithAlert());
        }

        [HttpPost]
        public async Task<IActionResult> CreateAnswer(CreateAnswerViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return RedirectToAction("Index", new { id = viewModel.PostId }).PushAlert("Answer is Valid", Core.Enums.AlertType.Error);

            var createdAnswer = await answerService.CreateAnswer(viewModel.Content, viewModel.PostId);

            if (createdAnswer == null)
                return RedirectToAction("Index", new { id = viewModel.PostId }).PushAlert("Error occured when creating a answer");

            return RedirectToAction("Index", new { id = viewModel.PostId }).PushAlert("Answer added successfully");
        }
    }
}
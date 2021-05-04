using System;
using System.Linq;
using System.Threading.Tasks;
using MyForum.Core.Extensions;
using MyForum.Core.Params;
using MyForum.Core.Services.Interface;
using MyForum.Data;
using MyForum.Models.Domain.Post;
using MyForum.Models.Helpers.Pagination;

namespace MyForum.Core.Services
{
    public class PostService : IPostService
    {
        private readonly IAuthService authService;
        private readonly IDatabase database;
        private readonly IProfileService profileService;
        private readonly ICategoryService categoryService;

        public PostService(IAuthService authService, IDatabase database, IProfileService profileService,
        ICategoryService categoryService)
        {
            this.authService = authService;
            this.database = database;
            this.profileService = profileService;
            this.categoryService = categoryService;
        }
        public async Task<Post> GetPost(string postId)
        => await database.PostRepository.Get(postId);

        public async Task<PagedList<Post>> GetPosts(GetPostsParams postsParams)
        {
            var posts = await database.PostRepository.Fetch();
            if (postsParams.CategoryId != null)
                posts = posts.Where(p => p.CategoryId == postsParams.CategoryId);
            if (postsParams.Username != null)
            {
                posts = posts.Where(p => p.Username.ToLower().Contains(postsParams.Username.ToLower()));
            }
            if (postsParams.Title != null)
            {
                posts = posts.Where(p => p.Title.ToLower().Contains(postsParams.Title.ToLower()));
            }
            switch (postsParams.SortType)
            {
                case Enums.PostSortType.UpdatedAscending:
                    posts = posts.OrderBy(p => p.DateUpdated);
                    break;
                case Enums.PostSortType.UpdatedDescending:
                    posts = posts.OrderByDescending(p => p.DateUpdated);
                    break;
                default:
                    break;
            }

            return posts.ToPagedList<Post>(postsParams.PageNumber, postsParams.PageSize);
        }
        public async Task<Post> CreatePost(string title, string content, string categoryId)
        {
            var category = await categoryService.GetCategory(categoryId);
            var currentUser = await profileService.GetCurrentUser();

            if (currentUser == null || category == null)
                return null;

            var post = Post.Create(title, content, currentUser.Username);

            currentUser.Posts.Add(post);
            category.Posts.Add(post);

            return await database.Complete() ? post : null;
        }

        public async Task<bool> DeletePost(string postId)
        {
            var post = await GetPost(postId);
            var currentUser = await profileService.GetCurrentUser();
            if (post == null || post.UserId != currentUser.Id)
                return false;

            database.PostRepository.Delete(post);

            var xd =  await database.Complete();
            return xd;
        }


        public async Task<bool> UpdatePost(string postId, string title, string content, string categoryId)
        {
            var post = await database.PostRepository.Get(postId);

            if (post == null)
                return false;

            post.Title = title;
            post.Content = content;
            post.DateUpdated = DateTime.Now;
            post.CategoryId = categoryId;

            database.PostRepository.Update(post);

            return await database.Complete();
        }
    }
}
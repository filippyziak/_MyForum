using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using MyForum.Core.Enums;
using MyForum.Core.Extensions;
using MyForum.Core.Helpers;
using MyForum.Core.Services.Interface;
using MyForum.Data;
using MyForum.Models.Domain.Auth;

namespace MyForum.Core.Services
{
    public class ProfileService : IProfileService
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IDatabase database;
        private readonly IAuthService authService;

        public ProfileService(IHttpContextAccessor httpContextAccessor, IDatabase database, IAuthService authService)
        {
            this.httpContextAccessor = httpContextAccessor;
            this.database = database;
            this.authService = authService;
        }
        public async Task<User> GetCurrentUser()
        => await database.UserRepository.Get(this.httpContextAccessor.HttpContext.GetCurrentUserId());

        public async Task<bool> ChangePassword(string oldPassword, string newPassword)
        {
            var user = await GetCurrentUser();

            if (user == null || Utils.VerifyPassword(oldPassword, user.PasswordHash, user.PasswordSalt))
                return false;

            string passwordSalt = Utils.CreateSalt();
            string saltedPasswordHash = Utils.GenerateHash(newPassword, passwordSalt);

            user.SetPassword(saltedPasswordHash, passwordSalt);

            return await database.Complete();
        }
        public async Task<bool> ChangeEmail(string newEmail)
        {
            if (await authService.EmailExists(newEmail))
            {
                Alertify.Push("Email already exists", AlertType.Error);
                return false;
            }

            var user = await GetCurrentUser();
            user.SetEmail(newEmail);

            return await database.Complete();
        }

        public async Task<bool> DeleteAvatar()
        {
            throw new System.NotImplementedException();
        }

        public async Task<bool> SetAvatar(IFormFile photo)
        {
            throw new System.NotImplementedException();
        }
    }
}
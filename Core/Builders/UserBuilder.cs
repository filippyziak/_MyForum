using MyForum.Core.Builders.Interface;
using MyForum.Models.Domain.Auth;

namespace MyForum.Core.Builders
{
    public class UserBuilder : IUserBuilder
    {
        private readonly User user = new User();
        public User Build()
        => this.user;

        public IUserBuilder SetPassword(string passwordHash, string passwordSalt)
        {
            this.user.PasswordHash = passwordHash;
            this.user.PasswordSalt = passwordSalt;
            return this;
        }

        public IUserBuilder SetUserData(string username, string email)
        {
            this.user.Username = username;
            this.user.Email = email;
            return this;
        }
    }
}
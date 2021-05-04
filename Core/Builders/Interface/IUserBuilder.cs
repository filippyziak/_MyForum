using MyForum.Models.Domain.Auth;

namespace MyForum.Core.Builders.Interface
{
    public interface IUserBuilder : IBuilder<User>
    {
        IUserBuilder SetUserData(string username, string email);
        IUserBuilder SetPassword(string passwordHash, string passwordSalt);
    }
}
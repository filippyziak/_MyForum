using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using MyForum.Core.Builders;
using MyForum.Core.Enums;
using MyForum.Core.Helpers;
using MyForum.Core.Services.Interface;
using MyForum.Data;
using MyForum.Models.Domain.Auth;

namespace MyForum.Core.Services
{
    public class AuthService : IAuthService
    {
        private readonly IDatabase database;
        private readonly ITokenClaimsManager tokenClaimsManager;
        private readonly IEmailSender emailSender;
        public IConfiguration Configuration { get; }
        private readonly ICryptoService cryptoService;

        public AuthService(IDatabase database, ITokenClaimsManager tokenClaimsManager, IEmailSender emailSender,
        IConfiguration configuration, ICryptoService cryptoService)
        {
            this.database = database;
            this.tokenClaimsManager = tokenClaimsManager;
            this.emailSender = emailSender;
            Configuration = configuration;
            this.cryptoService = cryptoService;
        }


        public async Task<User> Login(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                Alertify.Push("Invalid username or password", AlertType.Error);
                return null;
            }

            var user = await database.UserRepository.Find(u => u.Username.ToLower() == username.ToLower());
            if (user == null)
            {
                Alertify.Push("Invalid username or password", AlertType.Error);
                return null;
            }

            if (user.IsBlocked)
            {
                Alertify.Push("Your account has been blocked", AlertType.Error);
                return null;
            }

            if (Utils.VerifyPassword(password, user.PasswordHash, user.PasswordSalt))
            {
                if (!user.EmailConfirmed)
                {
                    Alertify.Push("Account is not confirmed", AlertType.Error);
                    return null;
                }
                return user;
            }

            Alertify.Push("Invalid username or password", AlertType.Error);
            return null;
        }

        public async Task<User> Register(string username, string email, string password)
        {
            if (await EmailExists(email))
            {
                Alertify.Push("Email address already exists", AlertType.Error);
                return null;
            }

            if (await UsernameExists(username))
            {
                Alertify.Push("Username already exists", AlertType.Error);
                return null;
            }

            var passwordSalt = Utils.CreateSalt();
            var saltedPasswordHash = Utils.GenerateHash(password, passwordSalt);

            var user = new UserBuilder()
                        .SetUserData(username, email)
                        .SetPassword(saltedPasswordHash, passwordSalt)
                        .Build();

            database.UserRepository.Add(user);

            if (await database.Complete())
            {
                var registerToken = Token.Create(TokenType.Register);
                user.Tokens.Add(registerToken);

                if (await database.Complete())
                {
                    string encryptedToken = cryptoService.Encrypt(registerToken.Code);
                    string callbackUrl = $"{Configuration.GetValue<string>(AppSettingsKeys.ServerAddress)}Auth/ConfirmAccount?email={user.Email}&token={encryptedToken}";
                    return await emailSender.Send(Constants.ActivationAccountEmail(email, username, callbackUrl)) ? user : null;
                }
                return null;
            }
            else
            {
                Alertify.Push("Creating account failed", AlertType.Error);
                return null;
            }
        }
        public async Task<bool> ConfirmAccount(string email, string token)
        {
            if (string.IsNullOrEmpty(token))
                return false;

            token = cryptoService.Decrypt(token);
            var user = await database.UserRepository.GetUserWithToken(email);

            if (user == null)
                return false;

            var registerToken = user.Tokens.FirstOrDefault(t => t.Code == token && t.TokenType == TokenType.Register);
            if (registerToken == null)
                return false;

            user.ConfirmAccount();
            user.Tokens.Remove(registerToken);

            return await database.Complete();
        }
        public async Task Logout() => await tokenClaimsManager.LogoutWithClaims();
        public async Task<bool> EmailExists(string email) => await database.UserRepository.Find(u => u.Email.ToLower() == email.ToLower()) != null;
        public async Task<bool> UsernameExists(string username) => await database.UserRepository.Find(u => u.Username.ToLower() == username.ToLower()) != null;
    }
}
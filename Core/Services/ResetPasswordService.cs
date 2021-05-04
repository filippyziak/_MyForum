using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using MyForum.Core.Enums;
using MyForum.Core.Helpers;
using MyForum.Core.Services.Interface;
using MyForum.Data;
using MyForum.Models.Domain.Auth;

namespace MyForum.Core.Services
{
    public class ResetPasswordService : IResetPasswordService
    {
        private readonly IDatabase database;
        private readonly IEmailSender emailSender;
        private readonly ICryptoService cryptoService;

        public IConfiguration Configuration { get; }

        public ResetPasswordService(IDatabase database, IEmailSender emailSender, ICryptoService cryptoService,
        IConfiguration configuration)
        {
            this.database = database;
            this.emailSender = emailSender;
            this.cryptoService = cryptoService;
            Configuration = configuration;
        }
        public async Task<bool> SendResetPasswordCallback(string email, string newPassword)
        {
            var user = await database.UserRepository.Find(u => u.Email.ToLower() == email.ToLower());

            if (user == null)
                return false;

            var resetPasswordToken = Token.Create(TokenType.ResetPassword);
            user.Tokens.Add(resetPasswordToken);

            if (await database.Complete())
            {
                string encryptedToken = cryptoService.Encrypt(resetPasswordToken.Code);
                string encryptedPassword = cryptoService.Encrypt(newPassword);
                string callbackUrl = $"{Configuration.GetValue<string>(AppSettingsKeys.ServerAddress)}Auth/ConfirmResetPassword?email={user.Email}&token={encryptedToken}&newPassword={encryptedPassword}";
                return await emailSender.Send(Constants.ResetPasswordEmail(email, user.Username, callbackUrl));
            }
            return false;
        }
        public async Task<bool> ResetPassword(string email, string newPassword, string token)
        {
            var user = await database.UserRepository.GetUserWithToken(email);

            if (user == null)
                return false;

            token = cryptoService.Decrypt(token);

            var resetPasswordToken = user.Tokens.FirstOrDefault(t => t.Code == token && t.TokenType == TokenType.ResetPassword);

            if (resetPasswordToken == null)
                return false;

            newPassword = cryptoService.Decrypt(newPassword);
            string passwordSalt = Utils.CreateSalt();
            string saltedPasswordHash = Utils.GenerateHash(newPassword, passwordSalt);

            user.SetPassword(saltedPasswordHash, passwordSalt);

            if (await database.Complete())
            {
                user.Tokens.Remove(resetPasswordToken);
                return await database.Complete();
            }

            return false;
        }

    }
}
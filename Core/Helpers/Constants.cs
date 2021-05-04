using MyForum.Models.Helpers.Email;

namespace MyForum.Core.Helpers
{
    public static class Constants
    {
        #region values

        public const int TokenExpireTimeInDays = 7;
        public const int MaxUsernameLength = 16;
        public const int MinUsernameLength = 4;
        public const int MaxPasswordLength = 16;
        public const int MinPasswordLength = 6;
        public const int ResetPasswordTokenExpirationInHours = 2;
        public const int MaxPostTitleLength = 30;
        public const int MinPostTitleLength = 5;
        public const int MaxPostContentLength = 255;
        public const int MinPostContentLength = 10;

        public const int DefaultPageSize = 10;


        #endregion

        #region email
        public static EmailMessage ActivationAccountEmail(string email, string username, string callbackUrl)
                   => new EmailMessage(
                       email: email,
                       subject: "MyForum - activate your account",
                       message: $"<p>Hi <strong>{username}</strong>!</p>" +
                           BorderBottom +
                           $"<p>In order to activate your account on MyForum, click link below.<br><br>" +
                           $"Activation account link: <a href='{callbackUrl}'>LINK</a></p>" +
                           "<p>Best regards,<br>" +
                           "MyForum team</p>"
               );
        public static EmailMessage ResetPasswordEmail(string email, string username, string callbackUrl)
            => new EmailMessage(
                email: email,
                subject: "MyForum - Reset Your Password!",
                message: $"<p>Hi <strong>{username}</strong>!</p>" +
                    BorderBottom +
                    $"<p>In order to reset your password on MyForum, click link below.<br><br>" +
                    $"Password reset link: <a href='{callbackUrl}'>LINK</a></p>" +
                    "<p>Best regards,<br>" +
                    "MyForum team</p>"
        );
        private const string BorderBottom = "<div style=" + "\"border-bottom:2px solid black; margin-top: 5px; margin-bottom: 5px;\"" + "></div>";
        #endregion
    }
}
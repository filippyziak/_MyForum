using System;
using MyForum.Core.Enums;
using MyForum.Core.Helpers;

namespace MyForum.Models.Domain.Auth
{
    public class Token
    {
        public string Id { get; protected set; } = Utils.Id();
        public string Code { get; protected set; } = Utils.Token(length: 32);
        public DateTime DateCreated { get; protected set; } = DateTime.Now;
        public TokenType TokenType { get; protected set; }
        public string UserId { get; protected set; }
        public User User { get; protected set; }

        public static Token Create(TokenType tokenType) => new Token { TokenType = tokenType };
    }
}
using System;
using System.Collections.Generic;
using MyForum.Core.Helpers;
using MyForum.Models.Domain.Post;

namespace MyForum.Models.Domain.Auth
{
    public class User
    {
        public string Id { get; set; } = Utils.Id();
        public string Email { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string PasswordSalt { get; set; }
        public DateTime DateRegistered { get; set; } = DateTime.Now;
        public bool EmailConfirmed { get; set; }
        public bool IsBlocked { get; set; }

        public ICollection<Token> Tokens { get; set; } = new HashSet<Token>();
        public ICollection<MyForum.Models.Domain.Post.Post> Posts { get; set; } = new HashSet<MyForum.Models.Domain.Post.Post>();
        public ICollection<Answer> Answers { get; set; } = new HashSet<Answer>();

        public void ConfirmAccount()
        {
            EmailConfirmed = true;
        }
        public void SetPassword(string passwordHash, string passwordSalt)
        {
            PasswordHash = passwordHash;
            PasswordSalt = passwordSalt;
        }

        public void SetEmail(string newEmail)
        {
            Email = newEmail;
        }
    }
}
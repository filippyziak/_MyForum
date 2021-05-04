using System;
using System.Collections.Generic;
using MyForum.Core.Helpers;
using MyForum.Models.Domain.Auth;

namespace MyForum.Models.Domain.Post
{
    public class Post
    {
        public string Id { get; set; } = Utils.Id();
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.Now;
        public DateTime DateUpdated { get; set; } = DateTime.Now;
        public string UserId { get; set; }
        public string Username { get; set; }
        public string CategoryId { get; set; }

        public User User { get; set; }
        public Category Category { get; set; }
        public ICollection<Answer> Answers { get; set; } = new HashSet<Answer>();
        public static Post Create(string title, string content, string username) => new Post
        {
            Title = title,
            Content = content,
            Username = username
        };

        public void UpdateDate()
        {
            DateUpdated = DateTime.Now;
        }
    }
}
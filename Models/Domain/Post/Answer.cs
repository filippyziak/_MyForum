using System;
using MyForum.Core.Helpers;
using MyForum.Models.Domain.Auth;

namespace MyForum.Models.Domain.Post
{
    public class Answer
    {
        public string Id { get; set; } = Utils.Id();
        public string Content { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
        public string UserId { get; set; }
        public string Author { get; set; }
        public string PostId { get; set; }
        public User User { get; set; }
        public Post Post { get; set; }

        public static Answer Create(string content, string username) => new Answer { Content = content, Author = username };
    }
}
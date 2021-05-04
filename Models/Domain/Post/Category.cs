using System.Collections.Generic;
using MyForum.Core.Helpers;

namespace MyForum.Models.Domain.Post
{
    public class Category
    {
        public string Id { get; set; } = Utils.Id();
        public string Name { get; set; }
        public ICollection<Post> Posts { get; set; } = new HashSet<Post>();

        public static Category Create(string name) => new Category { Name = name };
    }
}
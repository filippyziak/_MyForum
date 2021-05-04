using System.Collections.Generic;
using System.Linq;
using MyForum.Models.Helpers.Pagination;

namespace MyForum.Core.Extensions
{
public static class CollectionExtensions
    {
        public static PagedList<T> ToPagedList<T>(this IEnumerable<T> collection, int pageNumber, int pageSize) where T : class
                    => PagedList<T>.Create(collection.AsQueryable(), pageNumber, pageSize);
    }
}
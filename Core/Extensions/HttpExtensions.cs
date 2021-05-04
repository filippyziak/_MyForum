using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace MyForum.Core.Extensions
{
    public static class HttpExtensions
    {
        public static bool IsLoggedIn(this HttpContext httpContext)
        => httpContext.User.Identity.IsAuthenticated;
        public static string GetCurrentUserId(this HttpContext httpContext)
        => httpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        public static string GetCurrentUsername(this HttpContext httpContext)
        => httpContext.User.FindFirst(ClaimTypes.Name)?.Value;


    }
}
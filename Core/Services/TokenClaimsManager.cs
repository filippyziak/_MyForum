using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using MyForum.Core.Services.Interface;
using MyForum.Models.Domain.Auth;

namespace MyForum.Core.Services
{
    public class TokenClaimsManager : ITokenClaimsManager
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        public TokenClaimsManager(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;

        }
        public async Task LoginWithClaims(User user, bool isPersistent = true, IEnumerable<Claim> customClaims = null)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Email, user.Email)
            };

            if (customClaims != null)
                claims.AddRange(customClaims);

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            await httpContextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity), new AuthenticationProperties { IsPersistent = isPersistent });
        }

        public async Task LogoutWithClaims()
        => await httpContextAccessor.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
    }
}
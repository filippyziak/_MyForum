using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using MyForum.Models.Domain.Auth;

namespace MyForum.Core.Services.Interface
{
    public interface ITokenClaimsManager
    {
        Task LoginWithClaims(User user, bool isPersistent = true, IEnumerable<Claim> customClaims = null);
        Task LogoutWithClaims();
    }
}
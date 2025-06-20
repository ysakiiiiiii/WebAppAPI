using Microsoft.AspNetCore.Identity;

namespace WebApp.APi.Repositories
{
    public interface ITokenRepository
    {
        string CreateJWTToken(IdentityUser user, List<string> roles);
    }
}

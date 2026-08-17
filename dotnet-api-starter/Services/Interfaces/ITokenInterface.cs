using Microsoft.AspNetCore.Identity;

namespace dotnet_api_starter.Services.Interfaces
{
    public interface ITokenInterface
    {
        string CreateJwtToken(IdentityUser user, List<string> roles);
    }
}

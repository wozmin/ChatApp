using ChatServer.Models;

namespace Services.Factory
{
    public interface IJwtTokenFactory
    {
        string GenerateJwt(string email, ApplicationUser user);
    }
}

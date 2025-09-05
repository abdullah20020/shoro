using System.Security.Claims;

namespace ApplicationLayer.Services
{
    public interface IJwtTokenService
    {
        string GenerateToken(string email, string? role, IEnumerable<Claim>? additionalClaims = null);
    }
}

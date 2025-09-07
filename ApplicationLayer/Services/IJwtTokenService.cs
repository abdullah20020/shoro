using ApplicationLayer.DTOs.Auth;
using DomianLayar.Entities;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace ApplicationLayer.Services
{
    public interface IJwtTokenService
    {
        /// <summary>
        /// Generates a JWT token for the given Identity user with all roles and required claims
        /// </summary>
        /// <param name="user">The Identity user</param>
        /// <param name="userManager">UserManager for fetching user roles</param>
        /// <returns>JwtTokenResponseDto containing the token and expiry time</returns>
        Task<JwtTokenResponseDto> GenerateTokenAsync(BaseUser user, UserManager<BaseUser> userManager);

        /// <summary>
        /// Generates a JWT token with custom claims (legacy method for backward compatibility)
        /// </summary>
        /// <param name="email">User email</param>
        /// <param name="role">User role</param>
        /// <param name="additionalClaims">Additional claims to include</param>
        /// <returns>JWT token string</returns>
        string GenerateToken(string email, string? role, IEnumerable<Claim>? additionalClaims = null);
    }
}

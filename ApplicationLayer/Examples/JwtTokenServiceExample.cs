using ApplicationLayer.DTOs.Auth;
using ApplicationLayer.Services;
using DomianLayar.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ApplicationLayer.Examples
{
    /// <summary>
    /// Example controller demonstrating how to use the JWT Token Service
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class JwtTokenServiceExample : ControllerBase
    {
        private readonly IJwtTokenService _jwtTokenService;
        private readonly UserManager<BaseUser> _userManager;

        public JwtTokenServiceExample(
            IJwtTokenService jwtTokenService,
            UserManager<BaseUser> userManager)
        {
            _jwtTokenService = jwtTokenService;
            _userManager = userManager;
        }

        /// <summary>
        /// Example: Generate a new JWT token for the current user
        /// </summary>
        [HttpPost("generate-token")]
        [Authorize]
        public async Task<ActionResult<JwtTokenResponseDto>> GenerateTokenForCurrentUser()
        {
            // Get current user
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Unauthorized("User not found");
            }

            // Generate comprehensive JWT token
            var tokenResponse = await _jwtTokenService.GenerateTokenAsync(user, _userManager);

            return Ok(tokenResponse);
        }

        /// <summary>
        /// Example: Generate a JWT token for a specific user by email
        /// </summary>
        [HttpPost("generate-token-for-user")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<JwtTokenResponseDto>> GenerateTokenForUser([FromBody] string email)
        {
            // Find user by email
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return NotFound($"User with email {email} not found");
            }

            // Generate comprehensive JWT token
            var tokenResponse = await _jwtTokenService.GenerateTokenAsync(user, _userManager);

            return Ok(tokenResponse);
        }

        /// <summary>
        /// Example: Get current user's claims from JWT token
        /// </summary>
        [HttpGet("current-user-claims")]
        [Authorize]
        public IActionResult GetCurrentUserClaims()
        {
            var claims = User.Claims.Select(c => new { c.Type, c.Value }).ToList();
            return Ok(claims);
        }

        /// <summary>
        /// Example: Check if current user has specific role
        /// </summary>
        [HttpGet("check-role/{role}")]
        [Authorize]
        public IActionResult CheckUserRole(string role)
        {
            var hasRole = User.IsInRole(role);
            return Ok(new { HasRole = hasRole, Role = role });
        }

        /// <summary>
        /// Example: Get user ID from JWT token
        /// </summary>
        [HttpGet("user-id")]
        [Authorize]
        public IActionResult GetUserId()
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            var subClaim = User.FindFirst("sub")?.Value;
            var jti = User.FindFirst("jti")?.Value;

            return Ok(new
            {
                UserId = userId,
                SubClaim = subClaim,
                Jti = jti
            });
        }
    }
}

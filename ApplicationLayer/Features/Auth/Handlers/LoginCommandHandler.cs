using MediatR;
using ApplicationLayer.DTOs.Auth;
using ApplicationLayer.Services;
using DomianLayar.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using ApplicationLayer.Features.Auth.Commands;

namespace ApplicationLayer.Features.Auth.Handlers
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, AuthResponseDto>
    {
        private readonly UserManager<BaseUser> _userManager;
        private readonly SignInManager<BaseUser> _signInManager;
        private readonly IJwtTokenService _jwtTokenService;
        private readonly ILogger<LoginCommandHandler> _logger;

        public LoginCommandHandler(
            UserManager<BaseUser> userManager,
            SignInManager<BaseUser> signInManager,
            IJwtTokenService jwtTokenService,
            ILogger<LoginCommandHandler> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtTokenService = jwtTokenService;
            _logger = logger;
        }

        public async Task<AuthResponseDto> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var loginDto = request.LoginDto;

            // Find user by email
            var user = await _userManager.FindByEmailAsync(loginDto.Email);
            if (user == null)
            {
                throw new InvalidOperationException("Invalid email or password");
            }

            // Check password
            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);
            if (!result.Succeeded)
            {
                throw new InvalidOperationException("Invalid email or password");
            }

            // Generate comprehensive JWT token with all user roles
            var tokenResponse = await _jwtTokenService.GenerateTokenAsync(user, _userManager);
            var refreshToken = Guid.NewGuid().ToString();

            // Get user roles for response
            var roles = await _userManager.GetRolesAsync(user);
            var primaryRole = roles.FirstOrDefault() ?? "User";

            _logger.LogInformation("User {Email} logged in successfully with roles: {Roles}", user.Email, string.Join(", ", roles));

            return new AuthResponseDto
            {
                IsSuccess = true,
                AccessToken = tokenResponse.AccessToken,
                RefreshToken = refreshToken,
                ExpiresAt = tokenResponse.ExpiryTime,
                UserId = user.Id,
                Email = user.Email ?? string.Empty,
                FullName = $"{user.FirstName} {user.LastName}".Trim(),
                Role = primaryRole
            };
        }
    }
}

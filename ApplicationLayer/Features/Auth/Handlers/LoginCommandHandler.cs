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

            // Get user roles
            var roles = await _userManager.GetRolesAsync(user);
            var role = roles.FirstOrDefault() ?? "User";

            // Generate JWT token
            var token = _jwtTokenService.GenerateToken(user.Email, role);
            var refreshToken = Guid.NewGuid().ToString();

            _logger.LogInformation("User {Email} logged in successfully with role {Role}", user.Email, role);

            return new AuthResponseDto
            {
                AccessToken = token,
                RefreshToken = refreshToken,
                ExpiresAt = DateTime.UtcNow.AddHours(1),
                UserId = user.Id,
                Email = user.Email,
                FullName = $"{user.FirstName} {user.LastName}",
                Role = role
            };
        }
    }
}

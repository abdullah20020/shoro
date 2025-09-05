using MediatR;
using ApplicationLayer.DTOs.Auth;
using ApplicationLayer.Services;
using DomianLayar.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace ApplicationLayer.Features.Auth.Handlers
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, AuthResponseDto>
    {
        private readonly UserManager<BaseUser> _userManager;
        private readonly IJwtTokenService _jwtTokenService;
        private readonly ILogger<RegisterCommandHandler> _logger;

        public RegisterCommandHandler(
            UserManager<BaseUser> userManager,
            IJwtTokenService jwtTokenService,
            ILogger<RegisterCommandHandler> logger)
        {
            _userManager = userManager;
            _jwtTokenService = jwtTokenService;
            _logger = logger;
        }

        public async Task<AuthResponseDto> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            var registerDto = request.RegisterDto;

            // Check if user already exists
            var existingUser = await _userManager.FindByEmailAsync(registerDto.Email);
            if (existingUser != null)
            {
                throw new InvalidOperationException("User with this email already exists");
            }

            // Create new user
            var user = new BaseUser
            {
                UserName = registerDto.Email,
                Email = registerDto.Email,
                FirstName = registerDto.FirstName,
                LastName = registerDto.LastName,
                Address = registerDto.Address,
                EmailConfirmed = true // For demo purposes
            };

            var result = await _userManager.CreateAsync(user, registerDto.Password);
            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                throw new InvalidOperationException($"Failed to create user: {errors}");
            }

            // Add role
            var roleResult = await _userManager.AddToRoleAsync(user, registerDto.Role);
            if (!roleResult.Succeeded)
            {
                var errors = string.Join(", ", roleResult.Errors.Select(e => e.Description));
                throw new InvalidOperationException($"Failed to add role: {errors}");
            }

            // Generate JWT token
            var token = _jwtTokenService.GenerateToken(user.Email, registerDto.Role);
            var refreshToken = Guid.NewGuid().ToString();

            _logger.LogInformation("User {Email} registered successfully with role {Role}", user.Email, registerDto.Role);

            return new AuthResponseDto
            {
                IsSuccess = true, // ✅ التعديل الأساسي
                AccessToken = token,
                RefreshToken = refreshToken,
                ExpiresAt = DateTime.UtcNow.AddHours(1),
                UserId = user.Id,
                Email = user.Email,
                FullName = $"{user.FirstName} {user.LastName}",
                Role = registerDto.Role
            };
        }
    }
}

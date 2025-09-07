# JWT Token Service Documentation

## Overview
The JWT Token Service provides comprehensive JWT token generation for ASP.NET Core Identity users with full role support and proper claim management.

## Features
- ✅ Generates JWT tokens for Identity users
- ✅ Includes all required claims: UserId (sub), Email, UserName, JTI
- ✅ Fetches and includes all user roles as Role claims
- ✅ Uses JwtSecurityTokenHandler for token creation
- ✅ Configurable via JwtSettings (SecretKey, Issuer, Audience)
- ✅ 1-hour token expiration (configurable)
- ✅ Returns token with expiry time in DTO format
- ✅ Backward compatibility with legacy method

## Interface: IJwtTokenService

```csharp
public interface IJwtTokenService
{
    /// <summary>
    /// Generates a JWT token for the given Identity user with all roles and required claims
    /// </summary>
    Task<JwtTokenResponseDto> GenerateTokenAsync(BaseUser user, UserManager<BaseUser> userManager);

    /// <summary>
    /// Generates a JWT token with custom claims (legacy method for backward compatibility)
    /// </summary>
    string GenerateToken(string email, string? role, IEnumerable<Claim>? additionalClaims = null);
}
```

## DTO: JwtTokenResponseDto

```csharp
public class JwtTokenResponseDto
{
    public string AccessToken { get; set; } = string.Empty;
    public DateTime ExpiryTime { get; set; }
    public string TokenType { get; set; } = "Bearer";
    public string Jti { get; set; } = string.Empty;
}
```

## Claims Included in Token

### Standard JWT Claims
- **sub**: User ID (from BaseUser.Id)
- **email**: User email address
- **unique_name**: Username
- **jti**: Unique token identifier (GUID)

### ASP.NET Core Identity Claims
- **NameIdentifier**: User ID
- **Email**: User email
- **Name**: Username
- **GivenName**: First name (if available)
- **Surname**: Last name (if available)
- **Role**: All user roles (multiple claims if user has multiple roles)

## Configuration

### JwtSettings Configuration
```json
{
  "JwtSettings": {
    "SecretKey": "YourSuperSecretKeyThatIsAtLeast32CharactersLong",
    "Issuer": "YourAppName",
    "Audience": "YourAppUsers",
    "ExpirationMinutes": 60
  }
}
```

### Service Registration
```csharp
// In Program.cs
builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));
builder.Services.AddScoped<IJwtTokenService, JwtTokenService>();
```

## Usage Examples

### 1. Basic Usage in Authentication Handler
```csharp
public async Task<AuthResponseDto> Handle(LoginCommand request, CancellationToken cancellationToken)
{
    var user = await _userManager.FindByEmailAsync(request.Email);
    
    // Generate comprehensive JWT token
    var tokenResponse = await _jwtTokenService.GenerateTokenAsync(user, _userManager);
    
    return new AuthResponseDto
    {
        AccessToken = tokenResponse.AccessToken,
        ExpiresAt = tokenResponse.ExpiryTime,
        // ... other properties
    };
}
```

### 2. Controller Usage
```csharp
[HttpPost("generate-token")]
[Authorize]
public async Task<ActionResult<JwtTokenResponseDto>> GenerateToken()
{
    var user = await _userManager.GetUserAsync(User);
    var tokenResponse = await _jwtTokenService.GenerateTokenAsync(user, _userManager);
    return Ok(tokenResponse);
}
```

### 3. Reading Claims from Token
```csharp
[HttpGet("user-info")]
[Authorize]
public IActionResult GetUserInfo()
{
    var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
    var email = User.FindFirst(ClaimTypes.Email)?.Value;
    var roles = User.FindAll(ClaimTypes.Role).Select(c => c.Value).ToList();
    
    return Ok(new { UserId = userId, Email = email, Roles = roles });
}
```

## Security Features

### Token Security
- **HMAC SHA256** signing algorithm
- **Unique JTI** for each token (prevents replay attacks)
- **Configurable expiration** (default 1 hour)
- **Proper issuer/audience validation**

### Role-Based Authorization
```csharp
[Authorize(Roles = "Admin,Lawyer")]
public IActionResult AdminOnlyEndpoint()
{
    return Ok("This endpoint is only accessible to Admins and Lawyers");
}
```

## Error Handling

The service includes proper error handling for:
- Invalid user objects
- Missing JWT configuration
- UserManager failures
- Token generation errors

## Testing

### Unit Test Example
```csharp
[Test]
public async Task GenerateTokenAsync_ShouldIncludeAllUserRoles()
{
    // Arrange
    var user = new BaseUser { Id = "123", Email = "test@example.com" };
    var userManager = MockUserManager();
    userManager.Setup(x => x.GetRolesAsync(user))
               .ReturnsAsync(new List<string> { "Admin", "Lawyer" });

    // Act
    var result = await _jwtTokenService.GenerateTokenAsync(user, userManager.Object);

    // Assert
    Assert.IsNotNull(result.AccessToken);
    Assert.IsTrue(result.ExpiryTime > DateTime.UtcNow);
    Assert.IsNotEmpty(result.Jti);
}
```

## Migration from Legacy Method

### Before (Legacy)
```csharp
var token = _jwtTokenService.GenerateToken(user.Email, role);
```

### After (New Method)
```csharp
var tokenResponse = await _jwtTokenService.GenerateTokenAsync(user, _userManager);
var token = tokenResponse.AccessToken;
```

## Best Practices

1. **Always use the async method** for new implementations
2. **Store JTI** for token revocation if needed
3. **Validate token expiry** on the client side
4. **Use HTTPS** in production
5. **Rotate secret keys** periodically
6. **Implement refresh token** mechanism for long-lived sessions

## Troubleshooting

### Common Issues
1. **"Invalid token"**: Check SecretKey, Issuer, Audience configuration
2. **"Token expired"**: Verify ExpirationMinutes setting
3. **"Missing roles"**: Ensure user has roles assigned in Identity
4. **"Claims not found"**: Use correct claim types when reading from token

### Debug Claims
```csharp
var claims = User.Claims.Select(c => new { c.Type, c.Value }).ToList();
// Log or return claims to debug token contents
```

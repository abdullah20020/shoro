using ApplicationLayer.Configuration;
using ApplicationLayer.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace ApplicationLayer.Extensions
{
    public static class JwtConfigurationExtensions
    {
        /// <summary>
        /// Configures JWT authentication and related services
        /// </summary>
        public static IServiceCollection AddJwtConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            // Configure JWT Settings
            var jwtSettings = configuration.GetSection("JwtSettings").Get<JwtSettings>();
            services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));

            // Configure JWT Authentication
            services.AddJwtAuthentication(jwtSettings);

            // Register JWT Services
            services.AddJwtServices();

            return services;
        }

        /// <summary>
        /// Configures JWT Authentication with Bearer tokens
        /// </summary>
        private static IServiceCollection AddJwtAuthentication(this IServiceCollection services, JwtSettings jwtSettings)
        {
            var key = Encoding.ASCII.GetBytes(jwtSettings?.SecretKey ?? "default-key");

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidIssuer = jwtSettings?.Issuer,
                    ValidateAudience = true,
                    ValidAudience = jwtSettings?.Audience,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };
            });

            return services;
        }

        /// <summary>
        /// Registers JWT-related services
        /// </summary>
        private static IServiceCollection AddJwtServices(this IServiceCollection services)
        {
            // Register JWT Token Service
            services.AddScoped<IJwtTokenService, JwtTokenService>();

            return services;
        }

        /// <summary>
        /// Configures Identity with custom options
        /// </summary>
        public static IServiceCollection AddIdentityConfiguration<TUser, TRole>(
            this IServiceCollection services,
            Action<IdentityOptions>? configureOptions = null)
            where TUser : IdentityUser
            where TRole : IdentityRole, new()
        {
            var identityBuilder = services.AddIdentity<TUser, TRole>(options =>
            {
                // Password settings
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequiredLength = 6;

                // User settings
                options.User.RequireUniqueEmail = true;
                options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";

                // Lockout settings
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

                // SignIn settings
                options.SignIn.RequireConfirmedEmail = false;
                options.SignIn.RequireConfirmedPhoneNumber = false;

                // Apply custom configuration if provided
                configureOptions?.Invoke(options);
            });

            return services;
        }

        /// <summary>
        /// Configures Authorization policies
        /// </summary>
        public static IServiceCollection AddAuthorizationPolicies(this IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                // Admin only policy
                options.AddPolicy("AdminOnly", policy =>
                    policy.RequireRole("Admin"));

                // Lawyer only policy
                options.AddPolicy("LawyerOnly", policy =>
                    policy.RequireRole("Lawyer"));

                // Law Firm only policy
                options.AddPolicy("LawFirmOnly", policy =>
                    policy.RequireRole("LawFirm"));

                // Client only policy
                options.AddPolicy("ClientOnly", policy =>
                    policy.RequireRole("Client"));

                // Lawyer or Law Firm policy
                options.AddPolicy("LawyerOrLawFirm", policy =>
                    policy.RequireRole("Lawyer", "LawFirm"));

                // Admin or Lawyer policy
                options.AddPolicy("AdminOrLawyer", policy =>
                    policy.RequireRole("Admin", "Lawyer"));

                // Any authenticated user policy
                options.AddPolicy("AuthenticatedUser", policy =>
                    policy.RequireAuthenticatedUser());
            });

            return services;
        }
    }
}

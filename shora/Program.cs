using ApplicationLayer.Extensions;
using ApplicationLayer.Features.Auth;
using DomianLayar.contract;
using DomianLayar.Entities;
using InfrastructureLayer;
using InfrastructureLayer.GenaricRepostory;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace shora
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container
            builder.Services.AddControllers();

            ApplicationLayer.Extensions.DatabaseConfigurationExtensions.AddDatabaseConfiguration(builder.Services, builder.Configuration);
            // Configure JWT Authentication and Services
            builder.Services.AddJwtConfiguration(builder.Configuration);

            // Configure Authorization Policies
            builder.Services.AddAuthorizationPolicies();

            // Configure AutoMapper
            builder.Services.AddAutoMapper(typeof(Program));

            // Configure MediatR
            builder.Services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(typeof(LoginCommand).Assembly);
            });

            // Configure Generic Repositories
            builder.Services.AddScoped(typeof(IGenaricRepostry<>), typeof(GenaricRepostry<>));
            builder.Services.AddScoped(typeof(ICommandRepository<>), typeof(GenaricRepostry<>));
            builder.Services.AddScoped(typeof(IQueryRepository<>), typeof(GenaricRepostry<>));

            // Register Generic Query and Command Handlers automatically for all entities
            builder.Services.AddGenericHandlers();

            // Configure Swagger
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo 
                { 
                    Title = "Shora API", 
                    Version = "v1",
                    Description = "A comprehensive legal platform API for connecting clients with lawyers and law firms"
                });

                c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = Microsoft.OpenApi.Models.ParameterLocation.Header,
                    Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
                {
                    {
                        new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                        {
                            Reference = new Microsoft.OpenApi.Models.OpenApiReference
                            {
                                Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] {}
                    }
                });
            });

            var app = builder.Build();

            // Seed Database
            await SeedDatabaseAsync(app);

            // HTTP request pipeline
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();

            await app.RunAsync();
        }

        private static async Task SeedDatabaseAsync(WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            
            string[] roleNames = { "Client", "Lawyer", "LawFirm", "Admin" };

            foreach (var roleName in roleNames)
            {
                if (!await roleManager.RoleExistsAsync(roleName))
                {
                    await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }
        }
    }
}

using ApplicationLayer.Features.Commands;
using ApplicationLayer.Features.Commend;
using ApplicationLayer.Features.Queries;
using DomianLayar.Entities;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace ApplicationLayer.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddGenericQueryHandlers(this IServiceCollection services)
        {
            // Get all entity types that inherit from BaseClass
            var entityTypes = Assembly.GetAssembly(typeof(BaseClass))
                ?.GetTypes()
                .Where(t => t.IsClass && !t.IsAbstract && typeof(BaseClass).IsAssignableFrom(t))
                .ToList() ?? new List<Type>();

            // Register GetAllQueryHandler for each entity type
            foreach (var entityType in entityTypes)
            {
                var getAllQueryType = typeof(GetAllQuery<>).MakeGenericType(entityType);
                var getAllQueryHandlerType = typeof(GetAllQueryHandler<>).MakeGenericType(entityType);
                var getAllInterfaceType = typeof(IRequestHandler<,>).MakeGenericType(getAllQueryType, typeof(IReadOnlyList<>).MakeGenericType(entityType));
                
                services.AddTransient(getAllInterfaceType, getAllQueryHandlerType);
            }

            // Register GetByIdQueryHandler for each entity type
            foreach (var entityType in entityTypes)
            {
                var getByIdQueryType = typeof(GetByIdQuery<>).MakeGenericType(entityType);
                var getByIdQueryHandlerType = typeof(GetByIdQueryHandler<>).MakeGenericType(entityType);
                var getByIdInterfaceType = typeof(IRequestHandler<,>).MakeGenericType(getByIdQueryType, entityType);
                
                services.AddTransient(getByIdInterfaceType, getByIdQueryHandlerType);
            }

            return services;
        }

        public static IServiceCollection AddGenericCommandHandlers(this IServiceCollection services)
        {
            // Get all entity types that inherit from BaseClass
            var entityTypes = Assembly.GetAssembly(typeof(BaseClass))
                ?.GetTypes()
                .Where(t => t.IsClass && !t.IsAbstract && typeof(BaseClass).IsAssignableFrom(t))
                .ToList() ?? new List<Type>();

            // Register CreateCommandHandler for each entity type
            foreach (var entityType in entityTypes)
            {
                var createCommandType = typeof(CreateCommand<>).MakeGenericType(entityType);
                var createCommandHandlerType = typeof(CreateCommandHandler<>).MakeGenericType(entityType);
                var createInterfaceType = typeof(IRequestHandler<,>).MakeGenericType(createCommandType, entityType);
                
                services.AddTransient(createInterfaceType, createCommandHandlerType);
            }

            // Register UpdateCommandHandler for each entity type
            foreach (var entityType in entityTypes)
            {
                var updateCommandType = typeof(UpdateCommand<>).MakeGenericType(entityType);
                var updateCommandHandlerType = typeof(UpdateCommandHandler<>).MakeGenericType(entityType);
                var updateInterfaceType = typeof(IRequestHandler<,>).MakeGenericType(updateCommandType, entityType);
                
                services.AddTransient(updateInterfaceType, updateCommandHandlerType);
            }

            // Register DeleteCommandHandler for each entity type
            foreach (var entityType in entityTypes)
            {
                var deleteCommandType = typeof(DeleteCommand<>).MakeGenericType(entityType);
                var deleteCommandHandlerType = typeof(DeleteCommandHandler<>).MakeGenericType(entityType);
                var deleteInterfaceType = typeof(IRequestHandler<,>).MakeGenericType(deleteCommandType, typeof(bool));
                
                services.AddTransient(deleteInterfaceType, deleteCommandHandlerType);
            }

            return services;
        }

        public static IServiceCollection AddGenericHandlers(this IServiceCollection services)
        {
            services.AddGenericQueryHandlers();
            services.AddGenericCommandHandlers();
            return services;
        }
    }
}

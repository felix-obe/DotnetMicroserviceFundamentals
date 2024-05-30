using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using ServiceBase.DependencyInjection.Attribute;

namespace ServiceBase.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddImplementations(this IServiceCollection services, params System.Reflection.Assembly[] assemblies)
    {
        foreach (var assembly in assemblies)
        {
            var typesWithAttribute = assembly.GetTypes()
                .Where(t => t.GetCustomAttributes<ImplementsOfAttribute>().Any())
                .ToList();

            foreach (var interfaceType in typesWithAttribute)
            {
                var attributes = interfaceType.GetCustomAttributes<ImplementsOfAttribute>();

                foreach (var attribute in attributes)
                {
                    services.AddTransient(interfaceType, attribute.ImplementationType);
                }
            }
        }

        return services;
    }
}
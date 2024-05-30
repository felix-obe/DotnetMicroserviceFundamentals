using System.Reflection;
using DotnetMicroServiceFundamentals.DependencyInjection.Attribute;
using Microsoft.Extensions.DependencyInjection;

namespace DotnetMicroServiceFundamentals.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddImplementations(this IServiceCollection services,
        params System.Reflection.Assembly[] assemblies)
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
                    services.AddTransient(interfaceType, attribute.ImplementationType);
            }
        }

        return services;
    }
}
using System.Reflection;
using DotnetMicroServiceFundamentals.Execution.Attribute;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DotnetMicroServiceFundamentals.Execution.ServiceExtension;

public static class ExecuteOnStartupExtensions
{
    public static IServiceCollection AddExecuteOnStartupServices(this IServiceCollection services)
    {
        var executeOnStartupTypes = Assembly.GetExecutingAssembly().GetTypes()
            .Where(t => t.GetCustomAttribute<ExecuteOnStartupAttribute>() != null);

        var addHostedServiceMethod = typeof(ServiceCollectionServiceExtensions)
            .GetMethods()
            .FirstOrDefault(m => m.Name == "AddHostedService" && m.IsGenericMethod);

        if (addHostedServiceMethod == null)
            throw new InvalidOperationException("Could not find the AddHostedService method.");

        foreach (var executeOnStartupType in executeOnStartupTypes)
            if (typeof(IHostedService).IsAssignableFrom(executeOnStartupType))
            {
                var genericMethod = addHostedServiceMethod.MakeGenericMethod(executeOnStartupType);
                genericMethod.Invoke(null, new object[] { services });
            }
            else
            {
                throw new InvalidOperationException(
                    $"Type {executeOnStartupType.Name} is marked with [ExecuteOnStartup] but does not implement IHostedService.");
            }

        return services;
    }
}
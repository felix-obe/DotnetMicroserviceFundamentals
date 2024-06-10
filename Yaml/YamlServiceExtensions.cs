using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using DotnetMicroServiceFundamentals.Yaml.Persistence;
using DotnetMicroServiceFundamentals.Yaml.Persistence.Implementation;

namespace DotnetMicroServiceFundamentals.Yaml;

public static class YamlServiceExtensions
{
    public static IServiceCollection AddYaml(this IServiceCollection services, IConfiguration configuration)
    {
        var yamlFilePath = Environment.GetEnvironmentVariable("YAML_FILE_PATH");

        // Validate the yaml file path
        if (string.IsNullOrEmpty(yamlFilePath))
            throw new ArgumentException(
                "Could not find .yml/.yaml File. Please add the path in YAML_FILE_PATH environment variable");

        // Add the YamlDocumentReaderPersistence as a singleton service
        services.AddSingleton<IYamlDocumentReaderPersistence>();

        return services;
    }
}


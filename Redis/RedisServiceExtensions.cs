using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace DotnetMicroServiceFundamentals.Redis;

public static class RedisServiceExtensions
{
    public static IServiceCollection AddRedis(this IServiceCollection services, IConfiguration configuration)
    {
        var redisConnectionString = Environment.GetEnvironmentVariable("REDIS_CONNECTION");

        // Validate the connection string
        if (string.IsNullOrEmpty(redisConnectionString))
            throw new ArgumentException("Redis connection string is not configured properly, make sure REDIS_CONNECTION environment variable is set.");

        // Create a ConnectionMultiplexer and add it to the services
        var redis = ConnectionMultiplexer.Connect(redisConnectionString);
        services.AddSingleton<IConnectionMultiplexer>(redis);

        return services;
    }
}
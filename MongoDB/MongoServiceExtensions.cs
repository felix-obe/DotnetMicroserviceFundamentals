using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace ServiceBase.MongoDB;

public static class MongoServiceExtensions
{
    public static IServiceCollection AddMongoDb(this IServiceCollection services, IConfiguration configuration)
    {
        // Get MongoDB connection string and password from configuration
        var mongoConnectionString = Environment.GetEnvironmentVariable("MONGO_CONNECTION_STRING");
        var mongoPassword = Environment.GetEnvironmentVariable("MONGO_PASSWORD");

        // Validate the connection string
        if (string.IsNullOrEmpty(mongoConnectionString))
        {
            throw new ArgumentException("MongoDB connection string is not configured properly.");
        }

        // Replace the placeholder with the actual password if provided
        mongoConnectionString = !string.IsNullOrEmpty(mongoPassword) ? mongoConnectionString.Replace("<password>", mongoPassword) :
            
            // If no password is provided, ensure there is no placeholder in the connection string
            mongoConnectionString.Replace(":<password>", string.Empty);

        // Create a MongoClient and add it to the services
        var mongoClient = new MongoClient(mongoConnectionString);
        services.AddSingleton(mongoClient);

        return services;
    }
}
# DotnetMicroServiceFundamentals

This is a sample .NET 8 library for microservice fundamentals.

## Installation

```sh
dotnet add package DotnetMicroServiceFundamentals
```
## Overview

This library provides easy-to-use extension methods to configure Redis and MongoDB connections in your .NET microservices.
It also offers a quick and easy to use way for dependency injection.

## Configuration

### Environment Variables

Ensure that you have the following environment variables set up in your environment:

For Redis:
- `REDIS_CONNECTION_STRING`: The connection string for your Redis instance.

For MongoDB:
- `MONGO_CONNECTION_STRING`: The connection string for your MongoDB instance. Replace `<password>` placeholder with the actual password if needed.
- `MONGO_PASSWORD` (optional): The password for your MongoDB instance, if required.

### Example `appsettings.json`

Here is an example of how you might configure your `appsettings.json` file for local development:

```json
{
  "Redis": {
    "ConnectionString": "your_redis_connection_string"
  },
  "MongoDB": {
    "ConnectionString": "mongodb://<username>:<password>@localhost:27017",
    "Password": ""
  }
}
```

## Usage

### Redis Configuration

To add Redis configuration to your .NET microservice, use the `AddRedis` extension method provided by the library.

1. **Add Environment Variables**: Ensure you have set the `REDIS_CONNECTION_STRING` environment variable.

2. **Modify `Program.cs`**:
    ```csharp
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using MyServiceLibrary;

    var builder = WebApplication.CreateBuilder(args);

    // Add environment variables to configuration
    builder.Configuration.AddEnvironmentVariables();

    // Add services to the container.
    builder.Services.AddControllers();

    // Add Redis configuration
    builder.Services.AddRedis(builder.Configuration);

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseDeveloperExceptionPage();
    }

    app.UseHttpsRedirection();
    app.UseAuthorization();
    app.MapControllers();
    app.Run();
    ```

### MongoDB Configuration

To add MongoDB configuration to your .NET microservice, use the `AddMongoDb` extension method provided by the library.

1. **Add Environment Variables**: Ensure you have set the `MONGO_CONNECTION_STRING` and optionally the `MONGO_PASSWORD` environment variables.

2. **Modify `Program.cs`**:
    ```csharp
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using MyServiceLibrary;

    var builder = WebApplication.CreateBuilder(args);

    // Add environment variables to configuration
    builder.Configuration.AddEnvironmentVariables();

    // Add services to the container.
    builder.Services.AddControllers();

    // Add MongoDB configuration
    builder.Services.AddMongoDb(builder.Configuration);

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseDeveloperExceptionPage();
    }

    app.UseHttpsRedirection();
    app.UseAuthorization();
    app.MapControllers();
    app.Run();
    ```

## Example Controllers

### Redis Test Controller

Create a test controller to verify the Redis connection:

```csharp
using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;

[ApiController]
[Route("[controller]")]
public class RedisTestController : ControllerBase
{
    private readonly IConnectionMultiplexer _redis;

    public RedisTestController(IConnectionMultiplexer redis)
    {
        _redis = redis;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var db = _redis.GetDatabase();
        var value = await db.StringGetAsync("test_key");
        return Ok(value.ToString());
    }
}
```

### MongoDB Test Controller

Create a test controller to verify the MongoDB connection:

```csharp
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;

[ApiController]
[Route("[controller]")]
public class MongoTestController : ControllerBase
{
    private readonly IMongoClient _mongoClient;

    public MongoTestController(IMongoClient mongoClient)
    {
        _mongoClient = mongoClient;
    }

    [HttpGet]
    public IActionResult Get()
    {
        var database = _mongoClient.GetDatabase("testdb");
        var collection = database.GetCollection<BsonDocument>("testcollection");
        var documents = collection.Find(_ => true).ToList();

        return Ok(documents);
    }
}
```
---

# Dependency Injection Automation for .NET Library

This library provides an easy way to automate dependency injection in .NET projects by using custom attributes and reflection. By simply adding an attribute to your interfaces, the library automatically registers implementations with the dependency injection container.

## Features

- Automatic registration of implementations using custom attributes.
- Scans the current assembly and referenced assemblies for decorated interfaces.
- Simplifies dependency injection setup.

## Getting Started


### Usage

1. **Decorate Your Interfaces**: Use the `[ImplementsOf]` attribute to specify the implementation type for your interfaces.

```csharp
[ImplementsOf(typeof(MyClass))]
public interface IMyInterface
{
    void DoSomething();
}

public class MyClass : IMyInterface
{
    public void DoSomething()
    {
        Console.WriteLine("MyClass implementation of IMyInterface.DoSomething");
    }
}
```

2. **Automatically Register Services**: During application startup, call the provided method to scan and register implementations.

**Program Configuration**:

In your `Program.cs`, use the library's method to automatically register services from the current assembly and its referenced assemblies:

```csharp
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

// Create a new ServiceCollection
var services = new ServiceCollection();

// Scan and register implementations using the library's method
services.AddImplementationsFromAssembly(Assembly.GetExecutingAssembly());

// Build the service provider
var serviceProvider = services.BuildServiceProvider();

// Resolve an instance to test
var myService = serviceProvider.GetService<IMyInterface>();
myService?.DoSomething();
```

### Example

1. **Define Your Interface and Implementation**:

```csharp
[ImplementsOf(typeof(MyClass))]
public interface IMyInterface
{
    void DoSomething();
}

public class MyClass : IMyInterface
{
    public void DoSomething()
    {
        Console.WriteLine("MyClass implementation of IMyInterface.DoSomething");
    }
}
```

2. **Configure Services in Program.cs**:

```csharp
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

var services = new ServiceCollection();

// Scan and register implementations using the library's method
services.AddImplementationsFromAssembly(Assembly.GetExecutingAssembly());

// Build the service provider
var serviceProvider = services.BuildServiceProvider();

// Resolve an instance to test
var myService = serviceProvider.GetService<IMyInterface>();
myService?.DoSomething();
```

With this setup, any interface decorated with `[ImplementsOf]` will automatically have its implementation registered with the dependency injection container, simplifying the process of managing dependencies in your .NET projects.

## TODO
ExecuteOnStartup and ExecuteOnEvent attribute.

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.


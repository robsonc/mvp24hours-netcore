# ASP.NET Web API
Neste tópico você encontrará alguns recursos para acelerar a construção de serviços web com ASP.NET Web API.

## Serviços
```csharp
// Startup.cs => ConfigureServices(IServiceCollection)

/// essencial
services.AddMvp24HoursWebEssential(Configuration);

/// automapper
services.AddMvp24HoursMapService(assemblyMap: Assembly.GetExecutingAssembly());

/// json
services.AddMvp24HoursWebJson();

/// swagger
services.AddMvp24HoursWebSwagger("MyAPI");

/// compreension
services.AddMvp24HoursWebGzip();

/* 
Filtro: HATEOAS Filter
Descrição: Usado para sobre-escrever a resposta com metadados referentes aos recursos relacionados com o atual (crud), quando resources retornam objetos (sem IActionResult).
*/
services.AddMvp24HoursWebHATEOAS(options => { });

/* 
Filtro: Notification Filter
Descrição: Usado para sobre-escrever a resposta com mensagens de notificação quando resources retornam objetos (sem IActionResult).
*/
services.AddMvp24HoursWebNotification(options => { });

/// exception middleware
services.AddMvp24HoursWebExceptions(options => { });

/// cors middleware
services.AddMvp24HoursWebCors(options => { });
```

### Health Checks ([AspNetCore.Diagnostics.HealthChecks](https://github.com/Xabaril/AspNetCore.Diagnostics.HealthChecks))

```csharp

// Package Manager Console >
Install-Package AspNetCore.HealthChecks.UI.Client 3.1.2
/// SQLServer
Install-Package AspNetCore.HealthChecks.SqlServer 3.1.3
/// PostgreSQL
Install-Package AspNetCore.HealthChecks.NpgSql 3.1.1
/// MySQL
Install-Package AspNetCore.HealthChecks.MySql 3.2.0
/// MongoDB
Install-Package AspNetCore.HealthChecks.MongoDb 3.1.3
/// Redis
Install-Package AspNetCore.HealthChecks.Redis 3.1.2
/// RabbitMQ
Install-Package AspNetCore.HealthChecks.Rabbitmq 3.1.4

// Startup.cs

/// ConfigureServices(IServiceCollection)

services.AddHealthChecks()

    /// SQLServer
    .AddSqlServer(
        configuration.GetConnectionString("CustomerDbContext"),
        healthQuery: "SELECT 1;",
        name: "SqlServer",
        failureStatus: Microsoft.Extensions.Diagnostics.HealthChecks.HealthStatus.Degraded);

    /// MongoDB
    .AddMongoDb(
        configuration.GetConnectionString("MongoDbContext"),
        name: "MongoDb",
        failureStatus: Microsoft.Extensions.Diagnostics.HealthChecks.HealthStatus.Degraded);

    /// Redis
    .AddRedis(
        configuration.GetConnectionString("RedisDbContext"),
        name: "Redis",
        failureStatus: Microsoft.Extensions.Diagnostics.HealthChecks.HealthStatus.Degraded);

    /// RabbitMQ
    .AddRabbitMQ(
        configuration.GetConnectionString("RabbitMQContext"),
        name: "RabbitMQ",
        failureStatus: Microsoft.Extensions.Diagnostics.HealthChecks.HealthStatus.Degraded);

/// Configure(IApplicationBuilder,IWebHostEnvironment)

    app.UseEndpoints(endpoints =>
    {
        endpoints.MapControllers();
        endpoints.MapHealthChecks("/hc", new HealthCheckOptions
        {
            Predicate = _ => true,
            ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
        });
    });


```

## Aplicação / Middlewares

```csharp
// Startup.cs  => Configure(IApplicationBuilder,IWebHostEnvironment)

/// exception handlers
app.UseMvp24HoursExceptionHandling();

/// cors
app.UseMvp24HoursCors();

/// swagger
if (!env.IsProduction()) {
    app.UseMvp24HoursSwagger();
}

/// essencial
app.UseMvp24Hours();
```
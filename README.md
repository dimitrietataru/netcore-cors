# .NET Core - Cross-Origin Resource Sharing (CORS)

## Configure
``` csharp
public void ConfigureServices(IServiceCollection services)
{
    // ..

    var allowedOrigins = new string[] { "https://domain.com", "http://domain.com" };

    services.AddCors(corsOptions =>
    {
        corsOptions.AddPolicy(
            name: "permissive",
            configurePolicy: policyBuilder => policyBuilder
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

        corsOptions.AddPolicy(
            name: "restrictive",
            configurePolicy: policyBuilder => policyBuilder
                .WithOrigins(allowedOrigins)
                .AllowAnyMethod()
                .AllowAnyHeader());
    });

    // ..
}
```

## Usage
> Different policies can be applied to controllers, page models, or action methods with the *[EnableCors]* attribute.  
When the *[EnableCors]* attribute is applied to a controller, page model, or action method, and CORS is enabled in middleware, both policies are applied.  
**We recommend against combining policies**. Use the *[EnableCors]* attribute or middleware, not both in the same app.  
https://docs.microsoft.com/en-us/aspnet/core/security/cors?view=aspnetcore-3.1

``` csharp
public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
{
    // ..

    if (env.IsDevelopment())
    {
        app.UseCors("permissive");
    }

    app.UseCors("restrictive");
    
    // ..
}
```
``` csharp
[EnableCors("restrictive")]
public class TestController : ControllerBase
{
    // ...
}
```
``` csharp
public class TestController : ControllerBase
{
    [HttpGet]
    [Route("api/v1/test/restrictive")]
    [EnableCors("restrictive")]
    public IActionResult TestRestrictive()
    {
        return NoContent();
    }

    [HttpGet]
    [Route("api/v1/test/permissive")]
    [EnableCors("permissive")]
    public IActionResult TestPermissive()
    {
        return NoContent();
    }
}
```

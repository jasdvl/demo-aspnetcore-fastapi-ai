using AIxplorer.ComputerVision.ImageInterpretation.Services;

namespace AIxplorer.ComputerVision.ImageInterpretation.Bootstrap;

/// <summary>
/// Configures the middleware components for the application.
/// </summary>
/// <remarks>
/// This class is responsible for setting up the middleware pipeline, including routing, 
/// authentication, authorization, logging, and other middleware components that handle 
/// incoming requests and outgoing responses. It should be called after the dependency 
/// injection configuration to ensure that all services are available for the middleware.
/// </remarks>
public class MiddlewareConfigurator
{
    /// <summary>
    /// Configures the HTTP request pipeline for the application, including development-specific features like Swagger,
    /// OpenAPI, and gRPC Reflection. Sets up routing, CORS policies, and gRPC services.
    /// </summary>
    /// <param name="app">The <see cref="WebApplication"/> instance.</param>
    /// <param name="env">The <see cref="IWebHostEnvironment"/> representing the hosting environment.</param>
    public static void Configure(WebApplication app, IWebHostEnvironment env)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
            app.MapOpenApi();

            // Enables gRPC Reflection for tools like grpcurl to discover gRPC services. Useful for debugging.
            app.MapGrpcReflectionService();
        }

        // app.UseHttpsRedirection();
        app.UseCors("AllowOrigin");

        // app.ConfigureRoutes();

        app.MapGrpcService<ImageInterpretationServiceImpl>();
    }
}

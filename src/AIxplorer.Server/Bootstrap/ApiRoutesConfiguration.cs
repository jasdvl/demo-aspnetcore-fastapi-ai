using AIxplorer.Server.Services;
using Swashbuckle.AspNetCore.Annotations;

namespace AIxplorer.Server.Bootstrap;

/// <summary>
/// Provides configuration for API routes in the application.
/// </summary>
public static class ApiRoutesConfiguration
{
    /// <summary>
    /// Configures API routes for the application.
    /// </summary>
    /// <param name="app">The <see cref="WebApplication"/> instance.</param>
    /// <remarks>
    /// This configuration includes a REST API endpoint for image recognition. 
    /// However, it is currently deprecated and not in use, as REST has been replaced by gRPC for this functionality.
    /// </remarks>
    [Obsolete("This endpoint is deprecated. Use gRPC instead.")]
    public static void ConfigureRoutes(this WebApplication app)
    {
        // deprecated: REST Api
        app.MapPost("/api/computer-vision/image-interpretation",
            async (HttpContext httpContext, IFormFile file, IVisualDataInterpretationService imageRecognitionService) =>
            {
                throw new NotImplementedException();
            })
            .WithDescription("This endpoint processes the uploaded image for recognition using AI model")
            .WithMetadata(new SwaggerOperationAttribute(
                                                    "Image recognition",
                                                    "This endpoint processes the uploaded image for recognition using AI model"))
            .WithOpenApi()
            .DisableAntiforgery();
    }
}

using AIxplorer.Server.Services;
using Swashbuckle.AspNetCore.Annotations;

namespace AIxplorer.Server.Bootstrap;

public static class ApiRoutesConfiguration
{
    public static void ConfigureRoutes(this WebApplication app)
    {
        app.MapPost("/api/computer-vision/image-recognition",
                async (HttpContext httpContext, IFormFile file, IImageRecognitionService imageRecognitionService) =>
                {
                    if (file == null || file.Length == 0)
                    {
                        return Results.BadRequest("Image file parameter missing.");
                    }

                    try
                    {
                        var result = await imageRecognitionService.RecognizeImage(file);
                        return Results.Ok(result);
                    }
                    catch (ArgumentException ex)
                    {
                        return Results.BadRequest(ex.Message);
                    }
                    catch (Exception ex)
                    {
                        return Results.StatusCode(500);
                    }
                })
                .WithDescription("This endpoint processes the uploaded image for recognition using AI model")
                .WithMetadata(new SwaggerOperationAttribute(
                                                        "Image recognition",
                                                        "This endpoint processes the uploaded image for recognition using AI model"))
                .WithOpenApi()
                .DisableAntiforgery();
    }
}

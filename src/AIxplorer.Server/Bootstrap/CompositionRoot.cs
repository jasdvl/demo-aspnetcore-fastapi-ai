using AIxplorer.Server.Services;

namespace AIxplorer.Server.Bootstrap;

/// <summary>
/// The CompositionRoot class is responsible for setting up the application’s 
/// dependency injection and service registration.
/// </summary>
/// <remarks>
/// This class serves as the entry point for configuring the services that 
/// will be used throughout the application. It creates a <see cref="WebApplicationBuilder"/> 
/// instance and registers services, configuration settings, and environment 
/// information necessary for the application to function correctly.
/// </remarks>
public class CompositionRoot
{
    /// <summary>
    /// Creates a <see cref="WebApplicationBuilder"/> instance with the specified 
    /// <paramref name="options"/> and configures the application's services.
    /// </summary>
    /// <param name="options">The options to configure the web application.</param>
    /// <returns>A configured <see cref="WebApplicationBuilder"/> instance.</returns>
    public WebApplicationBuilder CreateBuilder(WebApplicationOptions options)
    {
        var builder = WebApplication.CreateBuilder(options);

        var environmentTarget = Environment.GetEnvironmentVariable("TARGET_ENVIRONMENT");

        builder.Configuration
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true);

        if (!string.IsNullOrEmpty(environmentTarget))
        {
            builder.Configuration.AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.{environmentTarget}.json", optional: true, reloadOnChange: true);
        }

        ConfigureAndRegisterServices(builder.Services, builder.Configuration, builder.Environment);

        return builder;
    }

    private void ConfigureAndRegisterServices(IServiceCollection services, IConfiguration configuration, IWebHostEnvironment environment)
    {
        ConfigureServices(services, configuration);

        RegisterServices(services);
    }

    private void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        // About configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        // Add services to the container.
        // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
        services.AddOpenApi();

        TimeSpan preflightMaxAge = TimeSpan.FromSeconds(3600);

        services.AddCors(options =>
        {
            options.AddPolicy("AllowOrigin",
                builder =>
                {
                    builder.WithOrigins("http://localhost:7300")
                            .AllowAnyMethod()
                            .AllowAnyHeader()
                            .SetPreflightMaxAge(preflightMaxAge);
                });
        });

        //services.AddAntiforgery();
    }

    private void RegisterServices(IServiceCollection services)
    {
        services.AddScoped<IImageRecognitionService, ImageRecognitionService>();

    }
}

using AIxplorer.Core.AI.Vision.Interpretation;
using AIxplorer.Vision.Interpretation.Services;

namespace AIxplorer.Vision.Interpretation.Bootstrap;

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

    private void ConfigureAndRegisterServices(
                                            IServiceCollection services,
                                            IConfiguration configuration,
                                            IWebHostEnvironment environment)
    {
        ConfigureServices(services, configuration);

        var logger = services.BuildServiceProvider().GetRequiredService<ILogger<CompositionRoot>>();

        RegisterServices(logger, services, configuration);
    }

    private void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddLogging(loggingBuilder =>
        {
            loggingBuilder.AddConfiguration(configuration.GetSection("Logging"));
            loggingBuilder.AddConsole();
        });

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
                    builder.WithOrigins("http://localhost:4200", "http://localhost:10000", "http://host.docker.internal:10000")
                            .AllowAnyMethod()
                            .AllowAnyHeader()
                            .SetPreflightMaxAge(preflightMaxAge);
                });
        });

        services.AddGrpc(options =>
        {
            // 25 MB
            options.MaxReceiveMessageSize = 25 * 1024 * 1024;
        });
        services.AddGrpcReflection();
    }

    private void RegisterServices(ILogger<CompositionRoot> logger, IServiceCollection services, IConfiguration configuration)
    {
        // path for AI model
        var modelPath = configuration["ModelConfiguration:ModelPath"];
        if (string.IsNullOrEmpty(modelPath))
        {
            throw new ArgumentException("The model path is missing or empty. Please check the configuration setting 'ModelConfiguration:ModelPath'.", nameof(modelPath));
        }

        logger.LogInformation("Model path: {ModelPath}", modelPath);

        services.AddSingleton<ImageInterpreter>(sp =>
        {
            var loggerForImageInterpreter = sp.GetRequiredService<ILogger<ImageInterpreter>>();
            return new ImageInterpreter(loggerForImageInterpreter, modelPath);
        });

        services.AddScoped<IImageInterpretationService, ImageInterpretationServiceImpl>();
    }
}

using AIxplorer.AI.ComputerVision.ImageInterpretation;
using AIxplorer.Grpc.Contracts.ComputerVision;
using Grpc.Core;

namespace AIxplorer.ComputerVision.ImageInterpretation.Services;

/// <summary>
/// Interface for the <see cref="ImageInterpretationServiceImpl"/>, defining the contract for processing image interpretation requests.
/// </summary>
public interface IImageInterpretationService
{
    /// <summary>
    /// Processes an image and provides its interpretation result.
    /// </summary>
    /// <param name="request">The image interpretation request containing the image data.</param>
    /// <param name="context">The gRPC server call context.</param>
    /// <returns>A task representing the asynchronous operation, returning an <see cref="ImageInterpretationResult"/>.</returns>
    Task<ImageInterpretationResult> ProcessImage(ImageInterpretationRequest request, ServerCallContext context);
}

/// <summary>
/// Implementation of the gRPC <see cref="ImageInterpretationService"> for processing image data and returning textual descriptions.
/// </summary>
public class ImageInterpretationServiceImpl : ImageInterpretationService.ImageInterpretationServiceBase, IImageInterpretationService
{
    private readonly ILogger<ImageInterpretationServiceImpl> _logger;

    private readonly ImageInterpreter _imageAnalyzer;

    /// <summary>
    /// Initializes a new instance of the <see cref="ImageInterpretationServiceImpl"/> class.
    /// </summary>
    /// <param name="logger">The logger instance for logging service activity.</param>
    /// <param name="imageAnalyzer">The image analyzer used for interpreting image data.</param>
    public ImageInterpretationServiceImpl(
                                ILogger<ImageInterpretationServiceImpl> logger,
                                ImageInterpreter imageAnalyzer)
    {
        _logger = logger;
        _imageAnalyzer = imageAnalyzer;
    }

    /// <summary>
    /// Processes an image and returns its interpretation as a textual description.
    /// </summary>
    /// <param name="request">The request containing base64-encoded image data and metadata.</param>
    /// <param name="context">The gRPC server call context.</param>
    /// <returns>A task that represents the asynchronous operation, containing the interpretation result.</returns>
    public override async Task<ImageInterpretationResult> ProcessImage(ImageInterpretationRequest request, ServerCallContext context)
    {
        try
        {
            if (string.IsNullOrEmpty(request.ImageBase64))
            {
                throw new RpcException(new Status(StatusCode.InvalidArgument, "Image data are missing."));
            }

            string interpretation = await _imageAnalyzer.InterpretAsync("What do you recognize?", request.ImageBase64);

            var result = new ImageInterpretationResult
            {
                Description = interpretation
            };

            return result;
        }
        catch (Exception ex)
        {
            throw new RpcException(new Status(StatusCode.Internal, "Image processing failed.", ex));
        }
    }
}

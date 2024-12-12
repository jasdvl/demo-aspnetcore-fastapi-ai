namespace AIxplorer.Server.Services;

public interface IImageRecognitionService
{
    Task<ImageRecognitionResult> RecognizeImage(IFormFile image);
}

public class ImageRecognitionService : IImageRecognitionService
{
    ILogger<ImageRecognitionService> _logger;

    private readonly string _modelFilePath;

    public ImageRecognitionService(ILogger<ImageRecognitionService> logger)
    {
        _logger = logger;
    }

    public async Task<ImageRecognitionResult> RecognizeImage(IFormFile image)
    {
        if (image == null || image.Length == 0)
        {
            throw new ArgumentException("Missing file parameter.");
        }

        return new ImageRecognitionResult();
    }
}

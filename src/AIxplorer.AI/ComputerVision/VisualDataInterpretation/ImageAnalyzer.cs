using Microsoft.ML.OnnxRuntimeGenAI;
using System.Text;

namespace AIxplorer.AI.ComputerVision.VisualDataInterpretation;

/// <summary>
/// The <c>ImageAnalyzer</c> class provides functionality to interpret images using an ONNX runtime model.
/// </summary>
public class ImageAnalyzer : IDisposable
{
    private readonly string _modelPath;

    private readonly Model _model;

    private readonly MultiModalProcessor _processor;

    /// <summary>
    /// Initializes a new instance of the <see cref="ImageAnalyzer"/> class.
    /// </summary>
    /// <param name="modelPath">The file path to the ONNX model used for analysis.</param>
    public ImageAnalyzer(string modelPath)
    {
        _modelPath = modelPath;
        _model = new Model(_modelPath);
        _processor = new MultiModalProcessor(_model);
    }

    /// <summary>
    /// Releases all resources used by the <see cref="QuestionAnsweringAssistant"/> class.
    /// </summary>
    /// <remarks>
    /// This method is called to release unmanaged resources and perform any necessary cleanup 
    /// when the <see cref="QuestionAnsweringAssistant"/> is no longer needed.
    /// </remarks>
    public void Dispose()
    {
        _processor?.Dispose();
        _model?.Dispose();
    }

    /// <summary>
    /// Interprets an image provided as a Base64 string.
    /// </summary>
    /// <param name="base64Image">The Base64-encoded string of the image to be analyzed.</param>
    /// <returns>A <see cref="Task{String}"/> that represents the result of the image analysis.</returns>
    public async Task<string> Interpret(string question, string base64Image)
    {
        byte[] imageBytes = Convert.FromBase64String(base64Image);

        // Save the image to a temporary file to be compatible with the ONNX model input requirements.
        string filePath = Path.GetTempFileName();
        await File.WriteAllBytesAsync(filePath, imageBytes);

        // Load the image from the temporary file
        var images = Images.Load(new[] { filePath });

        using var tokenizerStream = _processor.CreateStream();

        string result = AnalyzeImage(question, images, _processor, _model, tokenizerStream);

        // Clean up temporary file
        try
        {
            File.Delete(filePath);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to delete temp file: {ex.Message}");
        }

        return result;
    }

    /// <summary>
    /// Analyzes an image using the provided prompt and processing tools.
    /// </summary>
    /// <param name="question">The textual prompt describing what to analyze in the image.</param>
    /// <param name="img">The <see cref="Images"/> object containing the loaded image data.</param>
    /// <param name="processor">The <see cref="MultiModalProcessor"/> used for image processing.</param>
    /// <param name="model">The <see cref="Model"/> instance representing the loaded ONNX model.</param>
    /// <param name="tokenizerStream">The <see cref="TokenizerStream"/> used for decoding tokens.</param>
    /// <returns>A string containing the analysis results.</returns>
    private string AnalyzeImage(string question, Images img, MultiModalProcessor processor, Model model, TokenizerStream tokenizerStream)
    {
        StringBuilder phiResponse = new StringBuilder();

        var systemPrompt = "Please answer questions in a clear and concise manner.";

        var fullPrompt = $"<|system|>{systemPrompt}<|end|><|user|><|image_1|>{question}<|end|><|assistant|>";

        var inputTensors = processor.ProcessImages(fullPrompt, img);
        using GeneratorParams generatorParams = new GeneratorParams(model);
        generatorParams.SetSearchOption("max_length", 3072);
        generatorParams.SetInputs(inputTensors);

        // Generate response
        using var generator = new Generator(model, generatorParams);
        while (!generator.IsDone())
        {
            generator.ComputeLogits();
            generator.GenerateNextToken();

            var seq = generator.GetSequence(0)[^1];
            var tokenString = tokenizerStream.Decode(seq);
            phiResponse.Append(tokenString);
        }

        return phiResponse.ToString();
    }
}

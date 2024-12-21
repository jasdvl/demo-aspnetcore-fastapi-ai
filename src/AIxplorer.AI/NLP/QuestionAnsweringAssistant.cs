using Microsoft.ML.OnnxRuntimeGenAI;

namespace AIxplorer.AI.NLP;

/// <summary>
/// The <c>QuestionAnsweringAssistant</c> class is responsible for generating responses to user questions
/// using an ONNX model and multi-modal processing.
/// </summary>
public class QuestionAnsweringAssistant : IDisposable
{
    private readonly string _modelPath;

    private readonly Model _model;

    private readonly MultiModalProcessor _processor;

    private readonly Tokenizer _tokenizer;

    /// <summary>
    /// Initializes a new instance of the <see cref="QuestionAnsweringAssistant"/> class.
    /// </summary>
    /// <param name="modelPath">The file path to the ONNX model used for generating answers.</param>
    public QuestionAnsweringAssistant(string modelPath)
    {
        _modelPath = modelPath;
        _model = new Model(_modelPath);
        _processor = new MultiModalProcessor(_model);
        _tokenizer = new Tokenizer(_model);
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
        _tokenizer?.Dispose();
        _processor?.Dispose();
        _model?.Dispose();
    }

    /// <summary>
    /// Generates an answer to a question using the loaded model and tokenizer.
    /// </summary>
    /// <param name="question">The question for which an answer is to be generated.</param>
    /// <returns>A <see cref="Task{String}"/> that represents the answer generated for the question.</returns>
    public async Task<string> GenerateAnswerAsync(string question)
    {
        try
        {
            return await Task.Run(() =>
            {
                return GenerateResponseFromQuestion(question, _tokenizer, _model);
            });
        }
        catch (Exception ex)
        {
            // TODO Log message
            Console.WriteLine($"Error occurred while generating answer: {ex.Message}");
            throw;
        }
    }

    /// <summary>
    /// Generates a response to the user's question using the provided tokenizer and model.
    /// </summary>
    /// <param name="question">The question to be answered.</param>
    /// <param name="tokenizer">The tokenizer used to encode and decode text for the model.</param>
    /// <param name="model">The ONNX model used to generate the response.</param>
    /// <returns>A string containing the generated answer.</returns>
    private string GenerateResponseFromQuestion(string question, Tokenizer tokenizer, Model model)
    {
        var systemPrompt = "You help people find information. Please answer questions in a clear and concise manner.";

        var fullPrompt = $"<|system|>{systemPrompt}<|end|><|user|>{question}<|end|><|assistant|>";
        var tokens = tokenizer.Encode(fullPrompt);
        string result = string.Empty;

        var generatorParams = new GeneratorParams(model);
        generatorParams.SetSearchOption("max_length", 2048);
        generatorParams.SetSearchOption("past_present_share_buffer", false);
        generatorParams.SetInputSequences(tokens);

        var generator = new Generator(model, generatorParams);
        while (!generator.IsDone())
        {
            generator.ComputeLogits();
            generator.GenerateNextToken();
            var outputTokens = generator.GetSequence(0);
            var newToken = outputTokens.Slice(outputTokens.Length - 1, 1);
            result += tokenizer.Decode(newToken);
        }

        return result;
    }
}

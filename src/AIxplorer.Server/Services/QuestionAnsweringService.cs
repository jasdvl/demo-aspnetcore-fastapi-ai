using AIxplorer.AI.NLP;
using AIxplorer.Grpc.Contracts.ComputerVision;
using Grpc.Core;

namespace AIxplorer.Server.Services;

public interface IQuestionAnsweringService
{
    Task<ImageInterpretationResult> GenerateAnswerAsync(ImageInterpretationRequest request, ServerCallContext context);
}

public class QuestionAnsweringService : IQuestionAnsweringService
{
    private readonly ILogger<QuestionAnsweringService> _logger;

    private readonly QuestionAnsweringAssistant _answeringAssistant;

    public QuestionAnsweringService(
                                ILogger<QuestionAnsweringService> logger,
                                QuestionAnsweringAssistant answeringAssistant)
    {
        _logger = logger;
        _answeringAssistant = answeringAssistant;
    }

    public async Task<ImageInterpretationResult> GenerateAnswerAsync(ImageInterpretationRequest request, ServerCallContext context)
    {
        throw new NotImplementedException();
    }
}

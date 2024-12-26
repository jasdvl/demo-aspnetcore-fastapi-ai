using AIxplorer.Core.AI.NLP;
using AIxplorer.Grpc.Contracts.NLP;
using Grpc.Core;

namespace AIxplorer.Nlp.QnA.Services;

public interface IQuestionAnsweringService
{
    Task<AnswerResponse> GenerateAnswerAsync(QuestionRequest request, ServerCallContext context);
}

public class QuestionAnsweringServiceImpl : QuestionAnsweringService.QuestionAnsweringServiceBase, IQuestionAnsweringService
{
    private readonly ILogger<QuestionAnsweringServiceImpl> _logger;

    private readonly QuestionAnsweringAssistant _answeringAssistant;

    public QuestionAnsweringServiceImpl(
                                ILogger<QuestionAnsweringServiceImpl> logger,
                                QuestionAnsweringAssistant answeringAssistant)
    {
        _logger = logger;
        _answeringAssistant = answeringAssistant;
    }

    public async Task<AnswerResponse> GenerateAnswerAsync(QuestionRequest request, ServerCallContext context)
    {
        throw new NotImplementedException();
    }
}

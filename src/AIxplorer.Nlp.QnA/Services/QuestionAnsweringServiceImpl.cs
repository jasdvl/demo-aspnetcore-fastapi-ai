using AIxplorer.Core.AI.NLP;
using AIxplorer.Grpc.Contracts.NLP;
using Grpc.Core;

namespace AIxplorer.Nlp.QnA.Services;

public interface IQuestionAnsweringService
{
    Task<AnswerResponse> GetAnswer(QuestionRequest request, ServerCallContext context);
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

    public override async Task<AnswerResponse> GetAnswer(QuestionRequest request, ServerCallContext context)
    {
        try
        {
            if (string.IsNullOrEmpty(request.Question))
            {
                throw new RpcException(new Status(StatusCode.InvalidArgument, "Question is missing."));
            }

            string answer = await _answeringAssistant.GenerateAnswerAsync(request.Question);

            var result = new AnswerResponse
            {
                Answer = answer
            };

            return result;
        }
        catch (Exception ex)
        {
            throw new RpcException(new Status(StatusCode.Internal, "Image processing failed.", ex));
        }
    }
}

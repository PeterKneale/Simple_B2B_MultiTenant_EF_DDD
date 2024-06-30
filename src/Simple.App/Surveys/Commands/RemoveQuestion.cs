using Simple.Domain.Surveys;
using Simple.Domain.Surveys.Specifications;

namespace Simple.App.Surveys.Commands;

public static class RemoveQuestion
{
    public record Command(Guid SurveyId, Guid QuestionId) : IRequest;

    public class Validator : AbstractValidator<Command>
    {
        public Validator()
        {
            RuleFor(m => m.SurveyId).NotEmpty();
            RuleFor(m => m.QuestionId).NotEmpty();
        }
    }

    public class Handler(IRepository<Survey> surveys) : IRequestHandler<Command>
    {
        public async Task Handle(Command command, CancellationToken cancellationToken)
        {
            var surveyId = new SurveyId(command.SurveyId);
            var survey = await surveys.SingleOrDefaultAsync(new SurveyByIdSpec(surveyId), cancellationToken);
            if (survey == null)
            {
                PlatformException.ThrowNotFound(surveyId);
            }

            var questionId = new QuestionId(command.QuestionId);
            survey.RemoveQuestion(questionId);
            await surveys.UpdateAsync(survey, cancellationToken);
        }
    }
}